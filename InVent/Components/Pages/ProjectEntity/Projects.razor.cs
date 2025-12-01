using InVent.Components.Pages.ProductEntity;
using InVent.Data.Models;
using InVent.Services.ProductServices;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.ProjectEntity
{
    public partial class Projects
    {
        public List<Project> ProjectsList { get; set; } = [];
        public required MudTable<Project> Table { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await RefreshList();
            await base.OnInitializedAsync();
        }

        private async Task RefreshList()
        {
            await BeginLoadingProcess();
            try
            {
                var res = await ProjectService.GetAll();
                if (res.Success)
                    ProjectsList = res.Entities ?? [];
                else
                    HandleMessage(res.Message, res.Success);
            }
            catch (Exception err)
            {
                HandleMessage(err.Message, false);
            }
            await EndLoadingProcess();
        }

        public async Task OpenAddDialog(MouseEventArgs e)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.False };
            var parameters = new DialogParameters {
                { "Header" , "پروژه جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddProjectDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Project> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.False };
            var parameters = new DialogParameters {
                { "Project", e.Item },
                { "Header" , e.Item?.Number.ToString() }
            };

            await DialogService.ShowAsync<ViewProjectDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Project item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.False };
            var parameters = new DialogParameters {
                { "Project", item },
                { "Header" , "ویرایش پروژه" }
            };

            var dialog = await DialogService.ShowAsync<EditProjectDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                StateHasChanged();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Project item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Project", item },
                { "Header" , "حذف پروژه" },
                { "Message" , "آیا از حذف این پروژه اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteProjectDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}
