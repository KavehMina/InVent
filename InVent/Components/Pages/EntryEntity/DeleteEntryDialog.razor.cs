using InVent.Services.AttachmentServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InVent.Components.Pages.EntryEntity
{
    public partial class DeleteEntryDialog
    {
        [Inject]
        public AttachmentService AttachmentService { get; set; }
        private async Task Submit()
        {
            try
            {
                //this.Entry.Product = null;
                //this.Entry.Refinery = null;
                //this.Entry.DeliveryOrder = null;
                //this.Entry.Package = null;
                //this.Entry.Tanker = null;
                var res = await this.EntryService.Delete(this.Entry.Id);
                //deleting dispatch's attachments, from db, and local disk
                var attachments = await this.AttachmentService.GetAll(this.Entry.Id, "entry");
                foreach (var attachment in attachments.Entities ?? [])
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
