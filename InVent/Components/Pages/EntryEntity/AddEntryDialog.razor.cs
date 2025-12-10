using InVent.Data.Models;
using InVent.Services.AttachmentServices;
using InVent.Services.DeliveryOrderServices;
using InVent.Services.PackageServices;
using InVent.Services.ProductServices;
using InVent.Services.RefineryServices;
using InVent.Services.TankerServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.EntryEntity
{
    public partial class AddEntryDialog
    {
        [Inject]
        public required DeliveryOrderService DeliveryOrderService { get; set; }
        //[Inject]
        //public required RefineryService RefineryService { get; set; }
        //[Inject]
        //public required PackageService PackageService { get; set; }
        //[Inject]
        //public required ProductService ProductService { get; set; }
        [Inject]
        public required TankerService TankerService { get; set; }
        [Inject]
        public required AttachmentService AttachmentService { get; set; }

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

        private int? ProjectNumber { get; set; }

        //private Package Package { get; set; }
        //private List<Package> Packages { get; set; } = [];
        //private Product Product { get; set; }
        //private List<Product> Products { get; set; } = [];
        //private Refinery Refinery { get; set; }
        //private List<Refinery> Refineries { get; set; } = [];
        private Tanker Tanker { get; set; }
        private List<Tanker> Tankers { get; set; } = [];
        private DeliveryOrder DeliveryOrder { get; set; }
        private List<DeliveryOrder> DeliveryOrders { get; set; } = [];

        protected override async Task OnInitializedAsync()
        {
            try
            {
                DeliveryOrders = (await DeliveryOrderService.GetAll()).Entities ?? [];
                Tankers = (await TankerService.GetAll()).Entities ?? [];
            }
            catch (Exception err)
            {
                HandleMessage(err.Message, false);
            }
            await  base.OnInitializedAsync();
        }
        //protected override async void OnInitialized()
        //{
        //    try
        //    {
        //        //Refineries = (await RefineryService.GetAll()).Entities ?? [];
        //        DeliveryOrders = (await DeliveryOrderService.GetAll()).Entities ?? [];
        //        //Packages = (await PackageService.GetAll()).Entities ?? [];
        //        //Products = (await ProductService.GetAll()).Entities ?? [];
        //        Tankers = (await TankerService.GetAll()).Entities ?? [];
        //    }
        //    catch (Exception err)
        //    {
        //        HandleMessage(err.Message, false);
        //    }

        //    base.OnInitialized();
        //}

        //private async Task<IEnumerable<Package>> SearchPackages(string value, CancellationToken token)
        //{
        //    await Task.CompletedTask;
        //    if (value != null)
        //    {
        //        return Packages.Where(x => x.Name.Contains(value)).ToList();
        //    }
        //    return Packages;
        //}
        //private static string? PackageToString(Package package)
        //{
        //    return package?.Name;
        //}
        //private async Task<IEnumerable<Product>> SearchProducts(string value, CancellationToken token)
        //{
        //    await Task.CompletedTask;
        //    if (value != null)
        //    {
        //        return Products.Where(x => x.Name.Contains(value)).ToList();
        //    }
        //    return Products;
        //}
        //private static string? ProductToString(Product product)
        //{
        //    return product?.Name;
        //}

        //private async Task<IEnumerable<Refinery>> SearchRefineries(string value, CancellationToken token)
        //{
        //    await Task.CompletedTask;
        //    if (value != null)
        //    {
        //        return Refineries.Where(x => x.Name.Contains(value)).ToList();
        //    }
        //    return Refineries;
        //}
        //private static string? RefineryToString(Refinery refinery)
        //{
        //    return refinery?.Name;
        //}
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

        private readonly IList<IBrowserFile> files = [];

        private List<Attachment> Attachments = [];

        private async Task PrepareAttachments(Guid parentId)
        {
            this.ProjectNumber = this.DeliveryOrder.Project?.Number;
            foreach (var file in this.files)
            {
                var att = this.Attachments.Where(x => x.FileName == file.Name && x.FileSize == file.Size && x.ContentType == file.ContentType && x.LastModified == file.LastModified)
                    .FirstOrDefault();

                var folder = Path.Combine($"wwwroot/Attachments/Project-{this.ProjectNumber}/{att?.Category}");
                Directory.CreateDirectory(folder);

                var ext = file.Name.Split('.');
                var fileName = this.Tanker.Number + "-" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "." + ext.LastOrDefault(); ;
                var filePath = Path.Combine(folder, fileName);
                using var stream = File.Create(filePath);
                await file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024)
                    .CopyToAsync(stream);


                if (att != null)
                {
                    att.ParentId = parentId;
                    att.ParentType = "entry";
                    att.FilePath = $"/Attachments/Project-{this.ProjectNumber}/{att?.Category}/{fileName}";
                    att.FileName = fileName;

                }

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
                        //PackageTypeId = this.Package?.Id ?? this.DeliveryOrder.Project.Package.Id, //TEMPORARY
                        //ProductId = this.Product?.Id ?? this.DeliveryOrder.Project.Product.Id, //TEMPORARY
                        //RefineryId = this.Refinery?.Id ?? this.DeliveryOrder.Project.Product.Refinery.Id,   //TEMPORARY
                        TankerId = this.Tanker.Id,
                        RefineryEmpty = (int)this.RefineryEmpty,
                        RefineryFilled = (int)this.RefineryFilled,
                        WarehouseEmpty = (int)this.WarehouseEmpty,
                        WarehouseFilled = (int)this.WarehouseFilled,
                    };
                    var res = await this.EntryService.Add(tempEntry);
                    if (res.Success && res.Entities != null)
                    {
                        await this.PrepareAttachments(res.Entities.FirstOrDefault().Id);
                        foreach (var att in this.Attachments)
                        {
                            var attRes = await this.AttachmentService.Add(att);
                            this.HandleMessage(att.FileName, attRes.Success);
                        }
                        
                        await form.ResetAsync();
                        this.files.Clear();
                        this.Attachments.Clear();
                    }
                    this.HandleMessage(res.Message, res.Success);

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

        private string SetDifferenceColor()
        {
            switch (this.Difference)
            {
                case > 0:
                    return "background-color:palegreen";
                case < 0:
                    return "background-color:mistyrose";
                default:
                    return string.Empty;
            }
        }
    }
}
