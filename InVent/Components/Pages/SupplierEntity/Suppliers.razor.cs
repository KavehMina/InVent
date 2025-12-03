using InVent.Data.Models;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.SupplierEntity
{
    public partial class Suppliers
    {
        public List<Supplier> SuppliersList { get; set; } = [];
        public string? NewSupplierName { get; set; }
        public required MudTextField<string> Fieldref { get; set; }
        public required MudTable<Supplier> Table { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await RefreshList();
            await base.OnInitializedAsync();
        }
        private async Task DetectEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
                await this.Save();
        }



        public async Task Save()
        {
            await this.BeginLoadingProcess();
            if (!string.IsNullOrWhiteSpace(NewSupplierName))
            {
                try
                {
                    var NewSupplier = new Supplier { Name = this.NewSupplierName };
                    var res = await this.SupplierService.Add(NewSupplier);
                    this.HandleMessage(res.Message, res.Success);
                    if (res.Success)
                    {
                        this.NewSupplierName = string.Empty;
                        await this.Fieldref.BlurAsync();
                    }

                }
                catch (Exception err)
                {
                    this.HandleMessage(err.Message + "\n" + err.InnerException?.Message, false);
                }
            }
            else
            {
                this.HandleMessage("نام نمی‌تواند خالی باشد.", false);
            }
            await this.EndLoadingProcess();
            await this.RefreshList();
        }

        private async Task RefreshList()
        {
            await BeginLoadingProcess();
            try
            {
                var res = await this.SupplierService.GetAll();
                if (res.Success)
                    this.SuppliersList = res.Entities ?? [];
                else
                    HandleMessage(res.Message, res.Success);
            }
            catch (Exception err)
            {
                HandleMessage(err.Message, false);
            }
            await EndLoadingProcess();
        }

        public async Task OpenEditDialog(MouseEventArgs e, Supplier item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true , MaxWidth = MaxWidth.Small };
            var parameters = new DialogParameters {
                { "Supplier", item },
                { "Header" , "ویرایش تأمین‌کننده" }
            };

            var dialog = await DialogService.ShowAsync<EditSupplierDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null)
            {
                StateHasChanged();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Supplier item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Supplier", item },
                { "Header" , "حذف تأمین‌کننده" },
                { "Message" , "آیا از حذف این تأمین‌کننده اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteSupplierDialog>("", parameters, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await RefreshList();
            }
        }
    }
}
