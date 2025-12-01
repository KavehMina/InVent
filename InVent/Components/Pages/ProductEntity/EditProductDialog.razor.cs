using InVent.Components.Pages.RefineryEntity;
using InVent.Data.Models;
using InVent.Services.RefineryServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InVent.Components.Pages.ProductEntity
{
    public partial class EditProductDialog
    {
        [Inject]
        public required RefineryService RefineryService { get; set; }
        private List<Refinery> Refineries { get; set; } = [];

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
                    this.HandleMessage(res.Message, false);
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
        private async Task Submit()
        {
            await this.BeginLoadingProcess();
            await this.form.Validate();
            if (this.form.IsTouched && this.form.IsValid)
            {
                try
                {
                    var newProduct = new ProductDTO
                    {
                        Id = this.Product.Id,
                        Grade = this.Product.Grade ?? string.Empty,
                        Name = this.Product.Name ?? string.Empty,
                        RefineryId = this.Product.RefineryId
                    };
                    var res = await this.ProductService.Update(newProduct);
                    this.HandleMessage(res.Message, res.Success);
                }
                catch (Exception err)
                {
                    this.HandleMessage(err.Message, false);
                }
                MudDialog?.Close(DialogResult.Ok(true));
            }
            await this.EndLoadingProcess();
        }
    }
}
