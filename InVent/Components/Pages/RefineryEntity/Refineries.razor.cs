using InVent.Components.Pages.ProductEntity;
using InVent.Data.Models;
using InVent.Services.RefineryServices;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.RefineryEntity
{
    public partial class Refineries
    {
        public List<Refinery> RefineriesList { get; set; } = [];
        public required MudTable<Refinery> Table { get; set; }

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
                var res = await RefineryService.GetAll();
                if (res.Success)
                    RefineriesList = res.Entities ?? [];
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
                { "Header" , "پالایشگاه جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddRefineryDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Refinery> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Refinery", e.Item },
                { "Header" , e.Item?.Name }
            };

            await DialogService.ShowAsync<ViewRefineryDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Refinery item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Refinery", item },
                { "Header" , "ویرایش پالایشگاه" }
            };

            var dialog = await DialogService.ShowAsync<EditRefineryDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                StateHasChanged();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Refinery item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Refinery", item },
                { "Header" , "حذف پالایشگاه" },
                { "Message" , "آیا از حذف این پالایشگاه اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteRefineryDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}
