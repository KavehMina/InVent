using MudBlazor;

namespace InVent.Components.Pages.BankEntity
{
    public partial class EditBankDialog
    {
        private async Task Submit()
        {

            await BeginLoadingProcess();
            await form.Validate();
            if (form.IsTouched && form.IsValid)
            {
                try
                {
                    var res = await this.BankService.EditBank(this.Bank);
                    this.HandleMessage(res.Message, res.Success);

                }
                catch (Exception err)
                {
                    this.HandleMessage("ویرایش ناموفق" + "\n" + err.Message, false);
                }
                MudDialog?.Close(DialogResult.Ok(true));
            }
            else
            {

            }
        }
    }
}
