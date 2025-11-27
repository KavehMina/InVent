using InVent.Components.Pages.DispatchEntity;
using InVent.Data.Models;
using InVent.Data.Models.Miscellaneous;
using InVent.Services.AttachmentServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

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





        private void UploadFiles(IReadOnlyList<IBrowserFile> files)
        {
            if (files == null) this.InternalFiles.Clear();
            if (!IsMultiple && this.InternalFiles.Count > 0)
            {
                var f = InternalFiles[0];
                this.InternalFiles.Remove(f);
                this.Files.Remove(f);
                var att = this.Attachments.Where(x => x.FileName == f.Name && x.FileSize == f.Size && x.ContentType == f.ContentType)
                    .FirstOrDefault();
                if (att != null) this.Attachments.Remove(att);
            }

            foreach (var file in files ?? [])
            {
                this.InternalFiles.Add(file);
                this.Files.Add(file);
                this.Attachments.Add(new Attachment
                {
                    ParentType = "",
                    ParentId = Guid.Empty,
                    FilePath = string.Empty,
                    //the above are gonna be filled in parent component
                    ContentType = file.ContentType,
                    FileName = file.Name,
                    FileSize = file.Size,
                    Category = this.MapFileCategory()
                });
            }
        }



        private void RemoveFile(IBrowserFile file)
        {
            this.InternalFiles.Remove(file);
            this.Files.Remove(file);
            var att = this.Attachments.Where(x => x.FileName == file.Name && x.FileSize == file.Size && x.ContentType == file.ContentType)
                .FirstOrDefault();
            if (att != null) this.Attachments.Remove(att);
            this.StateHasChanged();
        }


        [Inject]
        IDialogService DialogService { get; set; }

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

            /*var dialog =*/
            await DialogService.ShowAsync<ViewDispatchAttachmentDialog>("", parameters, options);
            //var result = await dialog.Result;
            //if (result != null && !result.Canceled)
            //{
            //}
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
