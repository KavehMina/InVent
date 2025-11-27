using InVent.Services.AttachmentServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InVent.Components.Pages.DispatchEntity
{
    public partial class DeleteDispatchDialog
    {
        [Inject]
        public AttachmentService AttachmentService { get; set; }
        private async Task Submit()
        {
            try
            {

                var res = await this.DispatchService.Delete(this.Dispatch.Id);
                //deleting dispatch's attachments, from db, and local disk
                var attachments = await this.AttachmentService.GetAll(this.Dispatch.Id, "dispatch");
                foreach ( var attachment in attachments.Entities ?? [] )
                {
                    await this.AttachmentService.Delete(attachment.Id);
                    await this.AttachmentService.DeleteFile(attachment.FilePath);
                }
                //
                HandleMessage(res.Message, res.Success);
                if (res.Success)
                {
                    MudDialog?.Close(DialogResult.Ok(true));
                }
            }
            catch (Exception err)
            {
                HandleMessage("حذف ناموفق" + "\n" + err.Message, false);
            }

        }
    }
}
