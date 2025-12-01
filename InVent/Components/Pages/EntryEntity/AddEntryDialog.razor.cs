using InVent.Data.Models;
using InVent.Services.DeliveryOrderServices;
using InVent.Services.PackageServices;
using InVent.Services.ProductServices;
using InVent.Services.RefineryServices;
using InVent.Services.TankerServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.EntryEntity
{
    public partial class AddEntryDialog
    {
        [Inject]
        public required DeliveryOrderService DeliveryOrderService { get; set; }
        [Inject]
        public required RefineryService RefineryService { get; set; }
        [Inject]
        public required PackageService PackageService { get; set; }
        [Inject]
        public required ProductService ProductService { get; set; }
        [Inject]
        public required TankerService TankerService { get; set; }

        private DateTime? Date { get; set; } = DateTime.Today;
        private MudDatePicker _picker;

        private readonly List<MudTextField<int?>> TextFieldRefs = new(new MudTextField<int?>[6]);
        private int? Filled { get; set; }
        private int? Damaged { get; set; }

        private int? RefineryFilled { get; set; }
        private int? RefineryEmpty { get; set; }
        private int? RefineryNet { get; set; }

        private int? WarehouseFilled { get; set; }
        private int? WarehouseEmpty { get; set; }
        private int? WarehouseNet { get; set; }

        private int? Difference { get; set; }
        private Double? Average { get; set; }

        private Package Package { get; set; }
        private List<Package> Packages { get; set; } = [];
        private Product Product { get; set; }
        private List<Product> Products { get; set; } = [];
        private Refinery Refinery { get; set; }
        private List<Refinery> Refineries { get; set; } = [];
        private Tanker Tanker { get; set; }
        private List<Tanker> Tankers { get; set; } = [];
        private DeliveryOrder DeliveryOrder { get; set; }
        private List<DeliveryOrder> DeliveryOrders { get; set; } = [];


        protected override async void OnInitialized()
        {
            try
            {
                Refineries = (await RefineryService.GetAll()).Entities ?? [];
                DeliveryOrders = (await DeliveryOrderService.GetAll()).Entities ?? [];
                Packages = (await PackageService.GetAll()).Entities ?? [];
                Products = (await ProductService.GetAll()).Entities ?? [];
                Tankers = (await TankerService.GetAll()).Entities ?? [];
            }
            catch (Exception err)
            {
                HandleMessage(err.Message, false);
            }

            base.OnInitialized();
        }

        private async Task<IEnumerable<Package>> SearchPackages(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return Packages.Where(x => x.Name.Contains(value)).ToList();
            }
            return Packages;
        }
        private static string? PackageToString(Package package)
        {
            return package?.Name;
        }
        private async Task<IEnumerable<Product>> SearchProducts(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return Products.Where(x => x.Name.Contains(value)).ToList();
            }
            return Products;
        }
        private static string? ProductToString(Product product)
        {
            return product?.Name;
        }

        private async Task<IEnumerable<Refinery>> SearchRefineries(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return Refineries.Where(x => x.Name.Contains(value)).ToList();
            }
            return Refineries;
        }
        private static string? RefineryToString(Refinery refinery)
        {
            return refinery?.Name;
        }
        private async Task<IEnumerable<DeliveryOrder>> SearchDeliveryOrders(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return DeliveryOrders.Where(x => x.DeliveryOrderId.Contains(value)).ToList();
            }
            return DeliveryOrders;
        }
        private static string? DeliveryOrderToString(DeliveryOrder deliveryOrder)
        {
            return deliveryOrder?.DeliveryOrderId;
        }

        private async Task<IEnumerable<Tanker>> SearchTankers(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return Tankers.Where(x => x.DriverName.Contains(value)).ToList();
            }
            return Tankers;
        }
        private static string? TankerToString(Tanker tanker)
        {
            return tanker?.DriverName;
        }        

        private async Task DetectEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
                await Submit();
        }

        private void SetRefineryFilled(int? value)
        {
            this.RefineryFilled = value;
            this.SetRefineryNet();
        }

        private void SetRefineryEmpty(int? value)
        {
            this.RefineryEmpty = value;
            this.SetRefineryNet();
        }

        private void SetRefineryNet()
        {
            if (RefineryFilled != null && RefineryEmpty != null)
            {
                this.RefineryNet = this.RefineryFilled - this.RefineryEmpty;
            }
            else
            {
                this.RefineryNet = null;
            }
        }

        private void SetWarehouseFilled(int? value)
        {
            this.WarehouseFilled = value;
            this.SetWarehouseNet();
        }

        private void SetWarehouseEmpty(int? value)
        {
            this.WarehouseEmpty = value;
            this.SetWarehouseNet();
        }
        private void SetWarehouseNet()
        {
            if (this.WarehouseEmpty != null && this.WarehouseFilled != null)
            {
                this.WarehouseNet = this.WarehouseFilled - this.WarehouseEmpty;
            }
            else
            {
                this.WarehouseNet = null;
            }
        }

        private void SetDifference()
        {
            if (this.WarehouseNet != null && this.RefineryNet != null)
            {
                this.Difference = this.WarehouseNet - this.RefineryNet;
            }
            else
            {
                this.Difference = null;
            }

        }
        private void SetFilled(int? value)
        {
            this.Filled = value;
        }
        private void SetAverage()
        {
            if (this.Filled != null && this.WarehouseNet != null)
            {
                this.Average = (double)this.WarehouseNet / (this.Filled + this.Damaged);
            }
            else
            {
                this.Average = null;
            }
        }

        private async Task Submit()
        {
            await form.Validate();
            await BeginLoadingProcess();
            if (form.IsValid)
            {
                try
                {
                    var tempEntry = new EntryDTO()
                    {
                        Damaged = (int)this.Damaged,
                        Date = (DateTime)this.Date,
                        DeliveryOrderId = this.DeliveryOrder.Id,
                        Filled = (int)this.Filled,
                        PackageTypeId = this.Package.Id,
                        ProductId = this.Product.Id,
                        RefineryId = this.Refinery.Id,
                        TankerId = this.Tanker.Id,
                        RefineryEmpty = (int)this.RefineryEmpty,
                        RefineryFilled = (int)this.RefineryFilled,
                        WarehouseEmpty = (int)this.WarehouseEmpty,
                        WarehouseFilled = (int)this.WarehouseFilled,
                    };
                    var res = await this.EntryService.Add(tempEntry);
                    if (res.Success)
                    {
                        foreach (var field in this.TextFieldRefs)
                        {
                            await field.ResetAsync();
                            this.Tanker = null;
                        }
                        //await form.ResetAsync();

                    }
                    this.HandleMessage(res.Message, res.Success);



                    //MudDialog?.Close(DialogResult.Ok(true));

                }
                catch (Exception err)
                {
                    HandleMessage(err.Message, false);
                }
            }
            else
            {
                HandleMessage("invalid form", false);
            }
            await EndLoadingProcess();
        }

        private void SetToday()
        {
            this.Date = DateTime.Today;
            this._picker?.CloseAsync();
        }
    }
}
