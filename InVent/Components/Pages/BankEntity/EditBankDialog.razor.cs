using InVent.Data.Constants;
using InVent.Data.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InVent.Components.Pages.BankEntity
{
    public partial class EditBankDialog
    {
        [CascadingParameter]
        private IMudDialogInstance? MudDialog { get; set; }
        [Parameter]
        public required Bank Bank { get; set; }
        [Parameter]
        public string Header { get; set; } = string.Empty;

        private string TempBankName { get; set; } = string.Empty;

        protected override Task OnParametersSetAsync()
        {
            this.TempBankName = this.Bank.Name;
            return base.OnParametersSetAsync();
        }

        private async Task Submit()
        {
            if (!string.IsNullOrWhiteSpace(this.TempBankName))
            {
                try
                {
                    this.Bank.Name = this.TempBankName;
                    var res = await this.BankService.EditBank(this.Bank);
                    this.HandleMessage(res.Message, res.Success);
                }
                catch (Exception err)
                {
                    this.HandleMessage("ویرایش ناموفق" + "\n" + err.Message, false);
                }
                MudDialog?.Close(DialogResult.Ok(true));
            }
            else
            {
                this.TempBankName = this.Bank.Name;
                this.HandleMessage("نام نمی‌تواند خالی باشد.", false);
            }

        }
        private void Cancel() => MudDialog?.Cancel();
    }
}
