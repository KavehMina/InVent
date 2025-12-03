using InVent.Components.Pages.PortEntity;
using InVent.Data.Models;
using InVent.Services.CustomsServices;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.CustomsEntity
{
    public partial class CustomsList
    {
        public List<Customs> Customs { get; set; } = [];
        public required MudTable<Customs> Table { get; set; }

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
                var res = await this.CustomsService.GetAll();
                if (res.Success)
                    Customs = res.Entities ?? [];
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

            var options = new DialogOptions { CloseOnEscapeKey = true ,MaxWidth=MaxWidth.Small};
            var parameters = new DialogParameters {
                { "Header" , "گمرک اظهار جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddCustomsDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Customs> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small };
            var parameters = new DialogParameters {
                { "Customs", e.Item },
                { "Header" , e.Item?.Name }
            };

            await DialogService.ShowAsync<ViewCustomsDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Customs item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true , MaxWidth = MaxWidth.Small };
            var parameters = new DialogParameters {
                { "Customs", item },
                { "Header" , "ویرایش گمرک اظهار" }
            };

            var dialog = await DialogService.ShowAsync<EditCustomsDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                StateHasChanged();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Customs item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Customs", item },
                { "Header" , "حذف گمرک اظهار" },
                { "Message" , "آیا از حذف این گمرک اظهار اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteCustomsDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}
