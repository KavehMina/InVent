using MudBlazor;

namespace InVent.Components.Pages.PortEntity
{
    public partial class DeletePortDialog
    {
        private async Task Submit()
        {
            try
            {
                var res = await this.PortService.Delete(this.Port);
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
