using InVent.Services.AttachmentServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InVent.Components.Pages.DispatchEntity.Deprecated
{
    public partial class DeleteDispatchAttachmentDialog
    {
        [Inject]
        public required AttachmentService AttachmentService { get; set; }
        [Parameter]
        public required Guid DispatchId { get; set; }
        [Parameter]
        public required string FilePath { get; set; }
        private async Task Submit()
        {
            try
            {

                var res2 = await AttachmentService.DeleteFile(FilePath);
                var res = await AttachmentService.Delete(DispatchId);
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
