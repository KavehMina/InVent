using InVent.Components.Pages.BankEntity;
using InVent.Data.Models;
using InVent.Services.BankServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace InVent.Components.Pages.CarrierEntity
{
    public partial class AddCarrierDialog
    {
        [Inject]
        public required BankService BankService { get; set; }

        //private Carrier Carrier { get; set; } = new Carrier() { Name="", Phone = ""};
        private List<Bank> Banks { get; set; } = [];
        protected override async Task OnInitializedAsync()
        {
            this.Carrier = new Carrier() { Name = "", Phone = "" };
            try
            {
                var res = await this.BankService.GetAllBanks();
                if (res.Success)
                    this.Banks = res.Entities ?? [];
                else
                    this.HandleMessage(res.Message, res.Success);
            }
            catch (Exception err)
            {
                this.HandleMessage(err.Message, false);
            }
            await base.OnInitializedAsync();
        }
        private async Task<IEnumerable<Bank>> SearchBanks(string value, CancellationToken token)
        {
            await Task.CompletedTask;
            if (value != null)
            {
                return Banks.Where(x => x.Name.Contains(value)).ToList();
            }
            return Banks;
        }
        private static string? BankToString(Bank bank)
        {
            return bank?.Name;
        }

        private async Task DetectEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
                await this.Submit();
        }

        private async Task Submit()
        {
            await this.form.Validate();
            await this.BeginLoadingProcess();
            if (this.form.IsValid)
            {
                try
                {
                    this.Carrier.CarrierBankId = this.Carrier.Bank?.Id;
                    this.Carrier.Bank = null;
                    var res = await this.CarrierService.AddCarrier(this.Carrier);
                    if (res.Success)
                        await this.form.ResetAsync();
                    this.HandleMessage(res.Message, res.Success);

                    this.MudDialog?.Close(DialogResult.Ok(true));

                }
                catch (Exception err)
                {
                    this.HandleMessage(err.Message, false);
                }
            }
            else
            {
                this.HandleMessage("invalid form", false);
            }
            await this.EndLoadingProcess();
        }
    }
}
