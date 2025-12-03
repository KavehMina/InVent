using InVent.Components.Pages.DeliveryOrderEntity;
using InVent.Data.Models;
using InVent.Services.EntryServices;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.EntryEntity
{
    public partial class Entries
    {
        public List<Entry> EntriesList { get; set; } = [];
        public required MudTable<Entry> Table { get; set; }

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
                var res = await EntryService.GetAll();
                if (res.Success)
                    EntriesList = res.Entities ?? [];
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

            var options = new DialogOptions { CloseOnEscapeKey = true, FullScreen = true };
            var parameters = new DialogParameters
            {
                { "Header" , "ورودی جدید" }
            };

            var dialog = await DialogService.ShowAsync<AddEntryDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }

        public async Task OpenViewDialog(TableRowClickEventArgs<Entry> e)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, FullScreen = true };
            var parameters = new DialogParameters
            {
                { "Entry", e.Item },
                { "Header" , e.Item?.DeliveryOrder?.DeliveryOrderId }
            };

            await DialogService.ShowAsync<ViewEntryDialog>("", parameters, options);

        }

        public async Task OpenEditDialog(MouseEventArgs e, Entry item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true, FullScreen = true };
            var parameters = new DialogParameters
            {
                { "Entry", item },
                { "Header" , "ویرایش ورودی" }
            };

            var dialog = await DialogService.ShowAsync<EditEntryDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                await RefreshList();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Entry item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "Entry", item },
                { "Header" , "حذف ورودی" },
                { "Message" , "آیا از حذف این ورودی اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteEntryDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}
