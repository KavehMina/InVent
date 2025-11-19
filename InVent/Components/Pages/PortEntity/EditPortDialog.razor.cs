using MudBlazor;

namespace InVent.Components.Pages.PortEntity
{
    public partial class EditPortDialog
    {
        private async Task Submit()
        {
            await this.BeginLoadingProcess();
            await this.form.Validate();
            if (this.form.IsTouched && this.form.IsValid)
            {
                try
                {
                    var res = await this.PortService.Update(this.Port);
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
