using InVent.Components.Pages.BankEntity;
using InVent.Data.Models;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.CarrierEntity
{
    public partial class Carriers
    {
        public List<Carrier> CarriersList { get; set; } = [];
        public required MudTable<Carrier> Table { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.RefreshList();
            await base.OnInitializedAsync();
        }

        private async Task RefreshList()
        {
            await this.BeginLoadingProcess();
            try
            {
                var res = await this.CarrierService.GetAll();
                if (res.Success)
                    this.CarriersList = res.Entities ?? [];
                else
                    this.HandleMessage(res.Message, res.Success);
            }
            catch (Exception err)
            {
                this.HandleMessage(err.Message, false);
            }
            await this.EndLoadingProcess();
        }

        public async Task OpenAddDialog(MouseEventArgs e)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small };
            var parameters = new DialogParameters {
                { "Header" , "شرکت حمل‌ونقل جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddCarrierDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await this.RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Carrier> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small };
            var parameters = new DialogParameters {
                { "Carrier", e.Item },
                { "Header" , e.Item?.Name }
            };

            await DialogService.ShowAsync<ViewCarrierDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Carrier item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small };
            var parameters = new DialogParameters {
                { "Carrier", item },
                { "Header" , "ویرایش شرکت حمل‌ونقل" }
            };

            var dialog = await DialogService.ShowAsync<EditCarrierDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                this.StateHasChanged();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Carrier item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Carrier", item },
                { "Header" , "حذف شرکت حمل‌ونقل" },
                { "Message" , "آیا از حذف این شرکت حمل‌ونقل اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteCarrierDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await this.RefreshList();
            }
        }

    }
}
