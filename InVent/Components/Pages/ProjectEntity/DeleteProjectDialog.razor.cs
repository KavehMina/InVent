using MudBlazor;

namespace InVent.Components.Pages.ProjectEntity
{
    public partial class DeleteProjectDialog
    {
        private async Task Submit()
        {
            try
            {
                this.Project.Customer = null;
                this.Project.Customs = null;
                this.Project.Package = null;
                this.Project.Port = null;
                this.Project.Product = null;
                var res = await this.ProjectService.Delete(this.Project);
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
