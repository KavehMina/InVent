using InVent.Services.BankServices;
using InVent.Services.BookingServices;
using InVent.Services.CarrierServices;
using InVent.Services.CustomerServices;
using InVent.Services.CustomsServices;
using InVent.Services.DeliveryOrderServices;
using InVent.Services.DispatchServices;
using InVent.Services.EntryServices;
using InVent.Services.PackageServices;
using InVent.Services.PortServices;
using InVent.Services.ProductServices;
using InVent.Services.ProjectServices;
using InVent.Services.RefineryServices;
using InVent.Services.SupplierServices;
using InVent.Services.TankerServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace InVent.Components.Pages.Internals
{
    public class BaseComponent : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState>? AuthenticationState { get; set; }
        [Inject]
        public required NavigationManager NavigationManager { get; set; }
        [Inject]
        public required ISnackbar Snackbar { get; set; }
        [Inject]
        public required IDialogService DialogService { get; set; }


        public bool success;
        public string[] errors = [];
        public MudForm form;
        public bool ButtonDisabled => !form.IsValid;
        public bool Loading = false;

        protected override Task OnParametersSetAsync()
        {
            if (!this.AuthenticationState.Result.User.Identity.IsAuthenticated)
                this.NavigationManager.NavigateTo("/");
            return base.OnParametersSetAsync();
        }

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
    public class BaseCarrierComponent : BaseComponent
    {
        [Inject]
        public required CarrierService CarrierService { get; set; }
    }
    public class BaseProductComponent : BaseComponent
    {
        [Inject]
        public required ProductService ProductService { get; set; }
    }
    public class BaseCustomerComponent : BaseComponent
    {
        [Inject]
        public required CustomerService CustomerService { get; set; }
    }
    public class BaseCustomsComponent : BaseComponent
    {
        [Inject]
        public required CustomsService CustomsService { get; set; }
    }
    public class BaseRefineryComponent : BaseComponent
    {
        [Inject]
        public required RefineryService RefineryService { get; set; }
    }
    public class BasePortComponent : BaseComponent
    {
        [Inject]
        public required PortService PortService { get; set; }
    }
    public class BasePackageComponent : BaseComponent
    {
        [Inject]
        public required PackageService PackageService { get; set; }
    }
    public class BaseEntryComponent : BaseComponent
    {
        [Inject]
        public required EntryService EntryService { get; set; }
    }
    public class BaseProjectComponent : BaseComponent
    {
        [Inject]
        public required ProjectService ProjectService { get; set; }
    }
    public class BaseDeliveryOrderComponent : BaseComponent
    {
        [Inject]
        public required DeliveryOrderService DeliveryOrderService { get; set; }
    }
    public class BaseBookingComponent : BaseComponent
    {
        [Inject]
        public required BookingService BookingService { get; set; }
    }
    public class BaseDispatchComponent : BaseComponent
    {
        [Inject]
        public required DispatchService DispatchService { get; set; }
    }
    public class BaseSupplierComponent : BaseComponent
    {
        [Inject]
        public required SupplierService SupplierService { get; set; }
    }
}
