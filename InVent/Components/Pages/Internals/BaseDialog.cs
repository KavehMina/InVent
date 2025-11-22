using InVent.Data.Models;
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
using Microsoft.AspNetCore.Components;
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
    public class BaseProductDialog : BaseDialog
    {
        [Inject]
        public required ProductService ProductService { get; set; }
        [Parameter]
        public required Product Product { get; set; }
    }
    public class BasePortDialog : BaseDialog
    {
        [Inject]
        public required PortService PortService { get; set; }
        [Parameter]
        public required Port Port { get; set; }
    }
    public class BaseCustomsDialog : BaseDialog
    {
        [Inject]
        public required CustomsService CustomsService { get; set; }
        [Parameter]
        public required Customs Customs { get; set; }
    }
    public class BaseCustomerDialog : BaseDialog
    {
        [Inject]
        public required CustomerService CustomerService { get; set; }
        [Parameter]
        public required Customer Customer { get; set; }
    }
    public class BasePackageDialog : BaseDialog
    {
        [Inject]
        public required PackageService PackageService { get; set; }
        [Parameter]
        public required Package Package { get; set; }
    }
    public class BaseRefineryDialog : BaseDialog
    {
        [Inject]
        public required RefineryService RefineryService { get; set; }
        [Parameter]
        public required Refinery Refinery { get; set; }
    }
    public class BaseProjectDialog : BaseDialog
    {
        [Inject]
        public required ProjectService ProjectService { get; set; }
        [Parameter]
        public required Project Project { get; set; }
    }
    public class BaseDeliveryOrderDialog : BaseDialog
    {
        [Inject]
        public required DeliveryOrderService DeliveryOrderService { get; set; }
        [Parameter]
        public required DeliveryOrder DeliveryOrder { get; set; }
    }
    public class BaseEntryDialog : BaseDialog
    {
        [Inject]
        public required EntryService EntryService { get; set; }
        [Parameter]
        public required Entry Entry { get; set; }
    }
    public class BaseBookingDialog : BaseDialog
    {
        [Inject]
        public required BookingService BookingService { get; set; }
        [Parameter]
        public required Booking Booking { get; set; }
    }
    public class BaseDispatchDialog : BaseDialog
    {
        [Inject]
        public required DispatchService DispatchService { get; set; }
        [Parameter]
        public required Dispatch Dispatch { get; set; }
    }
}
