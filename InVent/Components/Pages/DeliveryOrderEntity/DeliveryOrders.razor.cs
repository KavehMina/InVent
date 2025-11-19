using InVent.Data.Models;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.DeliveryOrderEntity
{
    public partial class DeliveryOrders
    {
        public List<DeliveryOrder> DeliveryOrdersList { get; set; } = [];
        public required MudTable<DeliveryOrder> Table { get; set; }

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
                var res = await DeliveryOrderService.GetAll();
                if (res.Success)
                    DeliveryOrdersList = res.Entities ?? [];
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
            var parameters = new DialogParameters
            {
                { "Header" , "حواله جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddDeliveryOrderDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<DeliveryOrder> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "DeliveryOrder", e.Item },
                { "Header" , e.Item?.DeliveryOrderId }
            };

            await DialogService.ShowAsync<ViewDeliveryOrderDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, DeliveryOrder item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "DeliveryOrder", item },
                { "Header" , "ویرایش حواله" }
            };

            var dialog = await DialogService.ShowAsync<EditDeliveryOrderDialog>("", parameters);
            var result = await dialog.Result;
            if (result != null)
            {
                StateHasChanged();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, DeliveryOrder item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters 
            {
                { "DeliveryOrder", item },
                { "Header" , "حذف حواله" },
                { "Message" , "آیا از حذف این حواله اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteDeliveryOrderDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}
