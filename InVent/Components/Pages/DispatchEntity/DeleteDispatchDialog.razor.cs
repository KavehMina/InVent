using MudBlazor;

namespace InVent.Components.Pages.DispatchEntity
{
    public partial class DeleteDispatchDialog
    {
        private async Task Submit()
        {
            try
            {

                var res = await this.DispatchService.Delete(this.Dispatch.Id);
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
