using MudBlazor;

namespace InVent.Components.Pages.DeliveryOrderEntity
{
    public partial class DeleteDeliveryOrderDialog
    {
        private async Task Submit()
        {
            try
            {
                
                this.DeliveryOrder.Project= null;
                var res = await this.DeliveryOrderService.Delete(this.DeliveryOrder);
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
