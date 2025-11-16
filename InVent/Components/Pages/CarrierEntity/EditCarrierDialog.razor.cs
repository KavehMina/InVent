using InVent.Components.Pages.BankEntity;
using InVent.Data.Models;
using InVent.Services.BankServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InVent.Components.Pages.CarrierEntity
{
    public partial class EditCarrierDialog
    {
        [Inject]
        public required BankService BankService { get; set; }

        private List<Bank> Banks { get; set; } = [];

        protected override async Task OnInitializedAsync()
        {
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

        private async Task Submit()
        {
            await this.BeginLoadingProcess();

            if (this.form.IsTouched && this.form.IsValid)
            {
                try
                {
                    this.Carrier.CarrierBankId = this.Carrier.Bank?.Id;
                    var res = await this.CarrierService.UpdateCarrier(this.Carrier);
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
