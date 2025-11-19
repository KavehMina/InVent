using InVent.Components.Pages.CustomsEntity;
using InVent.Data.Models;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.CustomerEntity
{
    public partial class Customers
    {
        public List<Customer> CustomersList { get; set; } = [];
        public required MudTable<Customer> Table { get; set; }

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
                var res = await this.CustomerService.GetAll();
                if (res.Success)
                    CustomersList = res.Entities ?? [];
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
                { "Header" , "مشتری جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddCustomerDialog>("", parameters);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Customer> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Customer", e.Item },
                { "Header" , e.Item?.Name }
            };

            await DialogService.ShowAsync<ViewCustomerDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Customer item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Customer", item },
                { "Header" , "ویرایش مشتری" }
            };

            var dialog = await DialogService.ShowAsync<EditCustomerDialog>("", parameters);
            var result = await dialog.Result;
            if (result != null)
            {
                StateHasChanged();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Customer item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Customer", item },
                { "Header" , "حذف مشتری" },
                { "Message" , "آیا از حذف این مشتری اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteCustomerDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}
