using InVent.Components.Pages.RefineryEntity;
using InVent.Data.Models;
using InVent.Services.TankerServices;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.TankerEntity
{
    public partial class Tankers
    {
        public List<Tanker> TankersList { get; set; } = [];
        public required MudTable<Tanker> Table { get; set; }

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
                var res = await TankerService.GetAll();
                if (res.Success)
                    this.TankersList = res.Entities ?? [];
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
                { "Header" , "تانکر جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddTankerDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Tanker> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.False };
            var parameters = new DialogParameters {
                { "Tanker", e.Item },
                { "Header" , e.Item?.DriverName }
            };

            await DialogService.ShowAsync<ViewTankerDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Tanker item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.False };
            var parameters = new DialogParameters {
                { "Tanker", item },
                { "Header" , "ویرایش تانکر" }
            };

            var dialog = await DialogService.ShowAsync<EditTankerDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                StateHasChanged();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Tanker item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Tanker", item },
                { "Header" , "حذف تانکر" },
                { "Message" , "آیا از حذف این تانکر اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteTankerDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}
