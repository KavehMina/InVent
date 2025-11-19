
namespace InVent.Components.Pages.ProjectEntity
{
    public partial class ViewProjectDialog
    {
        private string StatusText => this.Project.Status == true ? "بسته" : "باز";
        
    }
}
