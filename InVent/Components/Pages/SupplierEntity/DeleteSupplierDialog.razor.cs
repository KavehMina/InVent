using MudBlazor;

namespace InVent.Components.Pages.SupplierEntity
{
    public partial class DeleteSupplierDialog
    {
        private async Task Submit()
        {
            try
            {
                var res = await this.SupplierService.Delete(this.Supplier.Id);
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
