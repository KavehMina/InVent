using InVent.Data.Models;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.CustomsEntity
{
    public partial class AddCustomsDialog
    {
        protected override void OnInitialized()
        {
            this.Customs = new Customs() { Number = 0, Name = "" };
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
                    var res = await this.CustomsService.Add(this.Customs);
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
