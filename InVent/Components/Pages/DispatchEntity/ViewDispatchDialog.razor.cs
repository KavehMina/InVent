
using InVent.Components.Pages.DispatchEntity.Deprecated;
using InVent.Data.Models;
using InVent.Services.AttachmentServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace InVent.Components.Pages.DispatchEntity
{
    public partial class ViewDispatchDialog
    {
        [Inject]
        public required AttachmentService AttachmentService { get; set; }
        private string ExportLabel => this.Dispatch.IsExport ? "صادراتی" : "داخلی";
        private readonly IList<IBrowserFile> files = [];

        private List<Attachment> Attachments = [];
        private List<Attachment> ExistingAttachments { get; set; } = [];

        protected override async Task OnInitializedAsync()
        {
            try
            {                
                this.ExistingAttachments = (await AttachmentService.GetAll(this.Dispatch.Id, "dispatch")).Entities ?? [];
            }
            catch (Exception err)
            {
                HandleMessage(err.Message, false);
            }
            await base.OnInitializedAsync();
        }

        //private async Task ViewAttachments(Attachment attachment)
        //{
        //    var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.False };
        //    var parameters = new DialogParameters
        //    {
        //        { "Attachment", attachment },
        //         { "Header" , attachment.FileName }
        //    };

        //    await DialogService.ShowAsync<ViewDispatchAttachmentDialog>("", parameters, options);
            
        //}
    }
}
