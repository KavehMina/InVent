using InVent.Services.AttachmentServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InVent.Components.Pages.Shared
{
    public partial class DeleteAttachmentDialog
    {
        [Inject]
        public required AttachmentService AttachmentService { get; set; }
        [Parameter]
        public required Guid ParentId { get; set; }
        [Parameter]
        public required string FilePath { get; set; }
        private async Task Submit()
        {
            try
            {

                var res2 = await AttachmentService.DeleteFile(FilePath);
                var res = await AttachmentService.Delete(ParentId);
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
