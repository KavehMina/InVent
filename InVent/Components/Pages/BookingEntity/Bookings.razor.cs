using InVent.Components.Pages.DeliveryOrderEntity;
using InVent.Data.Models;
using InVent.Services.DeliveryOrderServices;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.BookingEntity
{
    public partial class Bookings
    {
        public List<Booking> BookingsList { get; set; } = [];
        public required MudTable<Booking> Table { get; set; }

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
                var res = await BookingService.GetAll();
                if (res.Success)
                    BookingsList = res.Entities ?? [];
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
                { "Header" , "بوکینگ جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddBookingDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Booking> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "Booking", e.Item },
                { "Header" , e.Item?.Number.ToString() }
            };

            await DialogService.ShowAsync<ViewBookingDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Booking item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "Booking", item },
                { "Header" , "ویرایش بوکینگ" }
            };

            var dialog = await DialogService.ShowAsync<EditBookingDialog>("", parameters);
            var result = await dialog.Result;
            if (result != null)
            {
                await this.RefreshList();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Booking item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "Booking", item },
                { "Header" , "حذف بوکینگ" },
                { "Message" , "آیا از حذف این بوکینگ اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteBookingDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}
