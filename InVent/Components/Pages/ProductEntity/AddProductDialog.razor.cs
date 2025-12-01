using InVent.Components.Pages.BankEntity;
using InVent.Data.Models;
using InVent.Services.RefineryServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.ProductEntity
{
    public partial class AddProductDialog
    {
        [Inject]
        public required RefineryService RefineryService { get; set; }
        private List<Refinery> Refineries { get; set; } = [];
        private Refinery Refinery { get; set; }
        protected override void OnInitialized()
        {
            this.Product = new Product();
            base.OnInitialized();
        }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var res = await this.RefineryService.GetAll();
                if (res.Success)
                {
                    this.Refineries = res.Entities ?? [];
                }
                else
                {
                    this.HandleMessage(res.Message,false);
                }
            }
            catch (Exception err)
            {
                this.HandleMessage(err.Message, false);
            }
            await base.OnInitializedAsync();
        }

        private async Task<IEnumerable<Refinery>> SearchRefineries(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return this.Refineries.Where(x => x.Name.Contains(value)).ToList();
            }
            return Refineries;
        }
        private static string? RefineryToString(Refinery refinery)
        {
            return refinery?.Name;
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
                    var newProduct = new ProductDTO()
                    {
                        Grade = this.Product.Grade ?? string.Empty,
                        Name = this.Product.Name ?? string.Empty,
                        RefineryId = this.Refinery.Id,
                    };
                    var res = await this.ProductService.Add(newProduct);
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
