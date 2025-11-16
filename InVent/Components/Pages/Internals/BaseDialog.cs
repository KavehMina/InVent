using InVent.Data.Models;
using InVent.Services.BankServices;
using InVent.Services.CarrierServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.Internals
{
    public class BaseDialog : ComponentBase
    {
        [Inject]
        public required ISnackbar Snackbar { get; set; }
        [CascadingParameter]
        public IMudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public string Header { get; set; } = string.Empty;
        [Parameter]
        public string Message { get; set; } = string.Empty;

        public bool success;
        public string[] errors = [];
        public required MudForm form;
        public bool ButtonDisabled => !form.IsValid;
        public bool Loading = false;


        public async Task BeginLoadingProcess()
        {
            Loading = true;
            await Task.Delay(1);
        }
        public async Task EndLoadingProcess()
        {
            Loading = false;
            await Task.Delay(1);
        }

        
        public void Cancel() => MudDialog?.Cancel();

        public void HandleMessage(string Message, bool Success)
        {
            if (Snackbar != null)
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add(Message, Success ? Severity.Success : Severity.Error, cfg => cfg.VisibleStateDuration = 3000);
            }
        }
    }
    public class BaseBankDialog : BaseDialog
    {
        [Inject]
        public required BankService BankService { get; set; }
        [Parameter]
        public required Bank Bank { get; set; }
    }
    public class BaseCarrierDialog : BaseDialog
    {
        [Inject]
        public required CarrierService CarrierService { get; set; }
        [Parameter]
        public required Carrier Carrier { get; set; }
    }
}
