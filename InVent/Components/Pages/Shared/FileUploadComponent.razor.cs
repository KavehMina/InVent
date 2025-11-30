using InVent.Components.Pages.DispatchEntity;
using InVent.Data.Models;
using InVent.Services.AttachmentServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Linq;

namespace InVent.Components.Pages.Shared
{
    public partial class FileUploadComponent
    {
        [Parameter]
        public required string Title { get; set; }
        [Parameter]
        public required bool IsMultiple { get; set; }
        [Parameter]
        public required bool Required { get; set; }
        [Parameter]
        public required IList<IBrowserFile> Files { get; set; }
        [Parameter]
        public required List<Attachment> Attachments { get; set; }
        [Parameter]
        public List<Attachment>? ExistingAttachments { get; set; }
        public required IList<IBrowserFile> InternalFiles { get; set; } = [];
        private int MaxNumber => this.IsMultiple ? 10 : 1;

        private List<Attachment>? RelatedExistingAttachments => this.ExistingAttachments?.Where(x => x.Category == this.MapFileCategory()).ToList();
        private MudFileUpload<IReadOnlyList<IBrowserFile>> FileUploadRef { get; set; }




        private void UploadFiles(IReadOnlyList<IBrowserFile> files)
        {
            if (files == null) this.InternalFiles.Clear();
            if (!IsMultiple && this.InternalFiles.Count > 0)
            {
                var f = InternalFiles[0];
                this.InternalFiles.Remove(f);
                this.Files.Remove(f);
                var att = this.Attachments.Where(x => x.FileName == f.Name && x.FileSize == f.Size && x.ContentType == f.ContentType && x.Category == this.MapFileCategory())
                    .FirstOrDefault();

                if (att != null) this.Attachments.Remove(att);
            }

            foreach (var file in files ?? [])
            {
                var ex = this.Files.Where(x => x.Name == file.Name && x.Size == file.Size && x.ContentType == file.ContentType && x.LastModified == file.LastModified);
                if (ex.Any())
                {
                    this.HandleMessage("فایل تکراری", false);
                    break;
                }

                this.InternalFiles.Add(file);
                this.Files.Add(file);
                this.Attachments.Add(new Attachment
                {
                    LastModified = file.LastModified,
                    ParentType = "",
                    ParentId = Guid.Empty,
                    FilePath = string.Empty,
                    FileName = file.Name,
                    //the above are gonna be filled in parent component, filename will be overwritten
                    ContentType = file.ContentType,
                    FileSize = file.Size,
                    Category = this.MapFileCategory()
                });
            }
        }



        private void RemoveFile(IBrowserFile file)
        {
            this.InternalFiles.Remove(file);
            this.Files.Remove(file);
            var att = this.Attachments.Where(x => x.FileName == file.Name && x.FileSize == file.Size && x.ContentType == file.ContentType && x.Category == this.MapFileCategory())
                .FirstOrDefault();

            if (att != null) this.Attachments.Remove(att);
            this.FileUploadRef.Validate();
        }

        private string ValidateFileUpload(IReadOnlyList<IBrowserFile> arg)
        {
            if (this.ExistingAttachments == null) // adding new dispatch
            {
                return this.Required && this.InternalFiles.Count == 0 ? "اجباری" : string.Empty;
            }
            else // editing existing dispatch
            {
                return this.Required && this.ExistingAttachments.Count == 0 ? "اجباری" : string.Empty;
            }
        }


        //[Inject]
        //IDialogService DialogService { get; set; }

        private async Task DeleteAttachments(Attachment att)//Guid id, string filePath)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "DispatchId", att.Id },
                { "FilePath", att.FilePath },
                { "Header" , "حذف ضمیمه" },
                { "Message" , "آیا از حذف این ضمیمه اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteDispatchAttachmentDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                this.ExistingAttachments?.Remove(att);
                //this.ExistingAttachments = (await AttachmentService.GetAll(this.Dispatch.Id, "dispatch")).Entities ?? [];
            }
        }

        private async Task ViewAttachments(Attachment attachment)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.False };
            var parameters = new DialogParameters
            {
                { "Attachment", attachment },
                { "Header" , attachment.FileName }
            };

            await DialogService.ShowAsync<ViewDispatchAttachmentDialog>("", parameters, options);

        }

        private bool IsRequired()
        {
            if (this.ExistingAttachments == null)
                return this.Required;
            return this.Required && (this.RelatedExistingAttachments == null || this.RelatedExistingAttachments?.Count == 0);
        }

        private string MapFileCategory()
        {
            return this.Title switch
            {
                "بارنامه" => "lading",
                "قبض باسکول" => "scale",
                "رسید راننده" => "driver",
                "بیجک" => "lading",
                "سایر" => "misc",
                _ => string.Empty,
            };
        }

    }
}
