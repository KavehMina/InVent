using InVent.Data.Models;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.ProductEntity
{
    public partial class Products
    {
        public List<Product> ProductsList { get; set; } = [];
        public required MudTable<Product> Table { get; set; }

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
                var res = await ProductService.GetAll();
                if (res.Success)
                    ProductsList = res.Entities ?? [];
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
                { "Header" , "محصول جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddProductDialog>("", parameters);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Product> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Product", e.Item },
                { "Header" , e.Item?.Name }
            };

            await DialogService.ShowAsync<ViewProductDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Product item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Product", item },
                { "Header" , "ویرایش محصول" }
            };

            var dialog = await DialogService.ShowAsync<EditProductDialog>("", parameters);
            var result = await dialog.Result;
            if (result != null)
            {
                StateHasChanged();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Product item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Product", item },
                { "Header" , "حذف محصول" },
                { "Message" , "آیا از حذف این محصول اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteProductDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }

    }
}
