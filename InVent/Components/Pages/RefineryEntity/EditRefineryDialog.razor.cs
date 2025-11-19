using MudBlazor;

namespace InVent.Components.Pages.RefineryEntity
{
    public partial class EditRefineryDialog
    {
        private async Task Submit()
        {
            await BeginLoadingProcess();
            await form.Validate();
            if (form.IsTouched && form.IsValid)
            {
                try
                {
                    var res = await this.RefineryService.Update(this.Refinery);
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
