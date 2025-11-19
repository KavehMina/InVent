using MudBlazor;

namespace InVent.Components.Pages.CustomsEntity
{
    public partial class DeleteCustomsDialog
    {
        private async Task Submit()
        {
            try
            {
                var res = await this.CustomsService.Delete(this.Customs);
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
