using MudBlazor;

namespace InVent.Components.Pages.ProductEntity
{
    public partial class DeleteProductDialog
    {
        private async Task Submit()
        {
            try
            {
                var res = await this.ProductService.Delete(this.Product);
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
