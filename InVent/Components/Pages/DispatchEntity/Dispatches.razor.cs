using InVent.Components.Pages.DeliveryOrderEntity;
using InVent.Data.Models;
using InVent.Services.DeliveryOrderServices;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.DispatchEntity
{
    public partial class Dispatches
    {
        public List<Dispatch> DispatchesList { get; set; } = [];
        public required MudTable<Dispatch> Table { get; set; }

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
                var res = await DispatchService.GetAll();
                if (res.Success)
                    DispatchesList = res.Entities ?? [];
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
                { "Header" , "خروجی جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddDispatchDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Dispatch> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.False };
            var parameters = new DialogParameters
            {
                { "Dispatch", e.Item },
                { "Header" , "خروجی" }
            };

            await DialogService.ShowAsync<ViewDispatchDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Dispatch item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.False };
            var parameters = new DialogParameters
            {
                { "Dispatch", item },
                { "Header" , "ویرایش خروجی" }
            };

            var dialog = await DialogService.ShowAsync<EditDispatchDialog>("", parameters,options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Dispatch item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "Dispatch", item },
                { "Header" , "حذف خروجی" },
                { "Message" , "آیا از حذف این خروجی اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteDispatchDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}
