using MudBlazor;

namespace InVent.Components.Pages.CarrierEntity
{
    public partial class DeleteCarrierDialog
    {
        private async Task Submit()
        {
            try
            {
                var res = await this.CarrierService.DeleteCarrier(this.Carrier);
                HandleMessage(res.Message, res.Success);
                if (res.Success)
                {
                    MudDialog?.Close(DialogResult.Ok(true));
                }
            }
            catch (Exception err)
            {
                this.HandleMessage("حذف ناموفق" + "\n" + err.Message, false);
            }

        }
    }
}
