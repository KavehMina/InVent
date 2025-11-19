using MudBlazor;

namespace InVent.Components.Pages.CustomerEntity
{
    public partial class DeleteCustomerDialog
    {
        private async Task Submit()
        {
            try
            {
                var res = await this.CustomerService.Delete(this.Customer);
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
