using MudBlazor;

namespace InVent.Components.Pages.CustomerEntity
{
    public partial class EditCustomerDialog
    {
        private async Task Submit()
        {
            await BeginLoadingProcess();
            await form.Validate();
            if (form.IsTouched && form.IsValid)
            {
                try
                {
                    var res = await this.CustomerService.Update(this.Customer);
                    this.HandleMessage(res.Message, res.Success);
                }
                catch (Exception err)
                {
                    HandleMessage(err.Message, false);
                }
                MudDialog?.Close(DialogResult.Ok(true));
            }
            await EndLoadingProcess();
        }
    }
}
