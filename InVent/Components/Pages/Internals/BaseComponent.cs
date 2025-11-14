using InVent.Services.BankServices;
using InVent.Services.TankerServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InVent.Components.Pages.Internals
{
    public class BaseComponent : ComponentBase
    {
        [Inject]
        public required IServiceProvider ServiceProvider { get; set; }
        [Inject]
        public required NavigationManager NavigationManager { get; set; }
        [Inject]
        public ISnackbar? Snackbar { get; set; }
        public bool success;
        public string[] errors = [];
        public MudForm form;
        public bool ButtonDisabled => !form.IsValid;
        public bool Loading = false;


        public async Task BeginLoadingProcess()
        {
            Loading = true;
            await Task.Delay(1);
            //await InvokeAsync(() => this.StateHasChanged());
        }
        public async Task EndLoadingProcess()
        {
            Loading = false;
            await Task.Delay(1);
            //await InvokeAsync(() => this.StateHasChanged());
        }
        public void HandleMessage(string Message, bool Success)
        {
            if (Snackbar != null)
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                Snackbar.Add(Message, Success ? Severity.Success : Severity.Error, cfg => cfg.VisibleStateDuration = 3000);
            }
        }

    }
    public class BaseTankerComponent() : BaseComponent
    {
        [Inject]
        public required TankerService TankerService { get; set; }
    }

    public class BaseBankComponent() : BaseComponent
    {
        [Inject]
        public required BankService BankService { get; set; }
    }
}
