using InVent.Data.Constants;
using InVent.Data.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InVent.Components.Pages.BankEntity
{
    public partial class DeleteBankDialog
    {
        [CascadingParameter]
        private IMudDialogInstance? MudDialog { get; set; }
        [Parameter]
        public required Bank Bank { get; set; }
        [Parameter]
        public string Header { get; set; } = string.Empty;
        [Parameter]
        public string Message { get; set; } = string.Empty;
        private async Task Submit()
        {
            try
            {
                var res = await this.BankService.DeleteBank(this.Bank);
                HandleMessage(res.Message, res.Success);
                if (res.Success)
                {
                    MudDialog?.Close(DialogResult.Ok(true));
                }
            }
            catch (Exception err)
            {
                this.HandleMessage("حذف ناموفق" + "\n" + err.Message, false);
            }

        }
        private void Cancel() => MudDialog?.Cancel();
    }
}
