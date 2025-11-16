using InVent.Data.Constants;
using InVent.Data.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MudBlazor;

namespace InVent.Components.Pages.BankEntity
{
    public partial class Banks
    {
        //[Inject]
        //public required IDialogService DialogService { get; set; }
        public List<Bank> BanksList { get; set; } = [];
        public string? NewBankName { get; set; }
        public required MudTable<Bank> Table { get; set; }
        public required MudTextField<string> Fieldref { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await this.RefreshList();
            await base.OnInitializedAsync();
        }

        private async Task RefreshList()
        {
            await BeginLoadingProcess();
            try
            {
                var res = await BankService.GetAllBanks();
                if (res.Success)
                {
                    BanksList = res.Entities ?? [];
                }
                else
                {
                    HandleMessage(res.Message, res.Success);
                }

            }
            catch (Exception err)
            {
                HandleMessage(err.Message + Environment.NewLine + err.InnerException?.Message, false);
            }
            await EndLoadingProcess();
        }

        private async Task DetectEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
                await this.Save();
        }

        public async Task Save()
        {
            await this.BeginLoadingProcess();
            if (!string.IsNullOrWhiteSpace(NewBankName))
            {
                try
                {
                    var newBank = new Bank { Name = NewBankName.Trim() == "بلو" ? NewBankName + " " + "بانک" : "بانک" + " " + NewBankName };
                    var res = await this.BankService.AddBank(newBank);
                    this.HandleMessage(res.Message, res.Success);
                    if (res.Success)
                    {
                        this.NewBankName = string.Empty;
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


        public async Task OpenEditDialog(MouseEventArgs e, Bank item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters {
                { "Bank", item },
                { "Header" , "ویرایش بانک" }
            };

            var dialog = await DialogService.ShowAsync<EditBankDialog>("", parameters);
            var result = await dialog.Result;
            if (result != null)
            {
                this.StateHasChanged();
            }
        }
        public async Task OpenDeleteDialog(MouseEventArgs e, Bank item)
        {

            var options = new DialogOptions { CloseOnEscapeKey = true, NoHeader = true };
            var parameters = new DialogParameters {
                { "Bank", item },
                { "Header" , "حذف بانک" },
                { "Message" , "آیا از حذف این بانک اطمینان دارید؟" }
            };

            var dialog = await DialogService.ShowAsync<DeleteBankDialog>("", parameters);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                await this.RefreshList();
            }
        }

    }
}
