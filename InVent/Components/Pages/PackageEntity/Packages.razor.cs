using InVent.Components.Pages.CustomerEntity;
using InVent.Data.Models;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.PackageEntity
{
    public partial class Packages
    {
        public List<Package> PackageList { get; set; } = [];
        public required MudTable<Package> Table { get; set; }

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
                var res = await this.PackageService.GetAll();
                if (res.Success)
                    PackageList = res.Entities ?? [];
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

            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small };
            var parameters = new DialogParameters {
                { "Header" , "بسته‌بندی جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddPackageDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Package> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true , MaxWidth = MaxWidth.Small };
            var parameters = new DialogParameters {
                { "Package", e.Item },
                { "Header" , e.Item?.Name }
            };

            await DialogService.ShowAsync<ViewPackageDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Package item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true,  MaxWidth = MaxWidth.Small };
            var parameters = new DialogParameters {
                { "Package", item },
                { "Header" , "ویرایش بسته‌بندی" }
            };

            var dialog = await DialogService.ShowAsync<EditPackageDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                StateHasChanged();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Package item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Package", item },
                { "Header" , "حذف بسته‌بندی" },
                { "Message" , "آیا از حذف این بسته‌بندی اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeletePackageDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}
