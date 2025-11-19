using MudBlazor;

namespace InVent.Components.Pages.PackageEntity
{
    public partial class EditPackageDialog
    {
        private async Task Submit()
        {
            await BeginLoadingProcess();
            await form.Validate();
            if (form.IsTouched && form.IsValid)
            {
                try
                {
                    var res = await this.PackageService.Update(this.Package);
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
