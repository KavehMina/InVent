using InVent.Data.Models;
using InVent.Services.BankServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.ProductEntity
{
    public partial class AddProductDialog
    {
        protected override void OnInitialized()
        {
            this.Product = new Product() { Grade = "", Name = "" };
            base.OnInitialized();
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
                    var res = await this.ProductService.Add(this.Product);
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
