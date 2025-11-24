
using InVent.Data.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.PortEntity
{
    public partial class Ports
    {
        //[CascadingParameter]
        //private Task<AuthenticationState>? AuthenticationState { get; set; }
        //[Inject]
        //AuthenticationStateProvider AuthenticationStateProvider {  get; set; }
        public List<Port> PortsList { get; set; } = [];
        public required MudTable<Port> Table { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await RefreshList();
            await base.OnInitializedAsync();
        }

        protected override Task OnParametersSetAsync()
        {
            if (!this.AuthenticationState.Result.User.IsInRole("admin"))
                this.NavigationManager.NavigateTo("/");
            return base.OnParametersSetAsync();
        }

        private async Task RefreshList()
        {
            await BeginLoadingProcess();
            try
            {
                var res = await PortService.GetAll();
                if (res.Success)
                    PortsList = res.Entities ?? [];
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

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Header" , "بندر جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddPortDialog>("", parameters);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Port> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Port", e.Item },
                { "Header" , e.Item?.Name }
            };

            await DialogService.ShowAsync<ViewPortDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Port item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Port", item },
                { "Header" , "ویرایش بندر" }
            };

            var dialog = await DialogService.ShowAsync<EditPortDialog>("", parameters);
            var result = await dialog.Result;
            if (result != null)
            {
                StateHasChanged();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Port item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Port", item },
                { "Header" , "حذف بندر" },
                { "Message" , "آیا از حذف این بندر اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeletePortDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}
