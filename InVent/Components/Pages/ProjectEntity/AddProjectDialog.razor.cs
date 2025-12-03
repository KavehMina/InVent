using InVent.Data.Models;
using InVent.Services.CustomerServices;
using InVent.Services.CustomsServices;
using InVent.Services.PackageServices;
using InVent.Services.PortServices;
using InVent.Services.ProductServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.ProjectEntity
{
    public partial class AddProjectDialog
    {
        [Inject]
        public required CustomerService CustomerService { get; set; }
        [Inject]
        public required CustomsService CustomsService { get; set; }
        [Inject]
        public required PackageService PackageService { get; set; }
        [Inject]
        public required ProductService ProductService { get; set; }
        [Inject]
        public required PortService PortService { get; set; }

        private int? Number { get; set; }
        private int? Weight { get; set; }
        private int? PackageCount { get; set; }
        private Customer Customer { get; set; }
        private List<Customer> Customers { get; set; } = [];
        private Customs Customs { get; set; }
        private List<Customs> CustomsList { get; set; } = [];
        private Package Package { get; set; }
        private List<Package> Packages { get; set; } = [];
        private Product Product { get; set; }
        private List<Product> Products { get; set; } = [];
        private Port Port { get; set; }
        private List<Port> Ports { get; set; } = [];
        private bool Status { get; set; } = false;
        private string StatusText => this.Status == true ? "بسته" : "باز";

        protected override async void OnInitialized()
        {
            //this.Project = new ProjectDTO() { Grade = "", Name = "" };
            try
            {
                this.Customers = (await this.CustomerService.GetAll()).Entities ?? [];
                this.CustomsList = (await this.CustomsService.GetAll()).Entities ?? [];
                this.Packages = (await this.PackageService.GetAll()).Entities ?? [];
                this.Products = (await this.ProductService.GetAll()).Entities ?? [];
                this.Ports = (await this.PortService.GetAll()).Entities ?? [];
            }
            catch (Exception err)
            {
                this.HandleMessage(err.Message, false);
            }

            base.OnInitialized();
        }

        private async Task<IEnumerable<Customer>> SearchCustomers(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return this.Customers.Where(x => x.Name.Contains(value)).ToList();
            }
            return Customers;
        }
        private static string? CustomerToString(Customer customer)
        {
            return customer?.Name;
        }
        private async Task<IEnumerable<Package>> SearchPackages(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return this.Packages.Where(x => x.Name.Contains(value)).ToList();
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
                return this.Products.Where(x => x.Name.Contains(value)).ToList();
            }
            return Products;
        }
        private static string? ProductToString(Product product)
        {
            return product?.Name;
        }
        private async Task<IEnumerable<Port>> SearchPorts(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return this.Ports.Where(x => x.Name.Contains(value)).ToList();
            }
            return Ports;
        }
        private static string? PortToString(Port port)
        {
            return port?.Name;
        }

        private async Task<IEnumerable<Customs>> SearchCustoms(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return this.CustomsList.Where(x => x.Name.Contains(value)).ToList();
            }
            return CustomsList;
        }
        private static string? CustomsToString(Customs customs)
        {
            return customs?.Name;
        }

        private async Task DetectEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
                await Submit();
        }

        private async Task Submit()
        {
            await form.Validate();
            await BeginLoadingProcess();
            if (form.IsValid)
            {
                try
                {
                    var tempProject = new ProjectDTO()
                    {
                        CustomerId = this.Customer.Id,
                        CustomsId = this.Customs.Id,
                        PackageId = this.Package.Id,
                        PortId = this.Port.Id,
                        ProductId = this.Product.Id,
                        Status = this.Status,
                        Number = (int)this.Number,
                        ProjectWeight = (int)this.Weight,
                        PackageCount = (int)this.PackageCount,
                    };
                    var res = await this.ProjectService.Add(tempProject);
                    if (res.Success)
                        await form.ResetAsync();
                    this.HandleMessage(res.Message, res.Success);

                    MudDialog?.Close(DialogResult.Ok(true));

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
    }
}
