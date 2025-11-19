using MudBlazor;

namespace InVent.Components.Pages.CustomsEntity
{
    public partial class EditCustomsDialog
    {
        private async Task Submit()
        {
            await this.BeginLoadingProcess();
            await this.form.Validate();
            if (this.form.IsTouched && this.form.IsValid)
            {
                try
                {
                    var res = await this.CustomsService.Update(this.Customs);
                    this.HandleMessage(res.Message, res.Success);
                }
                catch (Exception err)
                {
                    this.HandleMessage(err.Message, false);
                }
                MudDialog?.Close(DialogResult.Ok(true));
            }
            await this.EndLoadingProcess();
        }
    }
}
