using MudBlazor;

namespace InVent.Components.Pages.RefineryEntity
{
    public partial class DeleteRefineryDialog
    {
        private async Task Submit()
        {
            try
            {
                var res = await this.RefineryService.Delete(this.Refinery);
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
