using MudBlazor;

namespace InVent.Components.Pages.PackageEntity
{
    public partial class DeletePackageDialog
    {
        private async Task Submit()
        {
            try
            {
                var res = await this.PackageService.Delete(this.Package);
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
