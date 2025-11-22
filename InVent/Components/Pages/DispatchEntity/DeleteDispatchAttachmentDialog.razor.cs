using InVent.Services.AttachmentServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InVent.Components.Pages.DispatchEntity
{
    public partial class DeleteDispatchAttachmentDialog
    {
        [Inject]
        public required AttachmentService AttachmentService { get; set; }
        [Parameter]
        public required Guid DispatchId { get; set; }
        private async Task Submit()
        {
            try
            {

                var res = await this.AttachmentService.Delete(this.DispatchId);
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
