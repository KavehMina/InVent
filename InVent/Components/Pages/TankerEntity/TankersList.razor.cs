using InVent.Data.Models;
using InVent.Services;
using InVent.Services.BankServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InVent.Components.Pages.TankerEntity
{
    public partial class TankersList()
    {
        [Inject]
        public required BankService BankService { get; set; }
        //private List<Tanker> Tankers { get; set; } = [];
        private List<TankerViewModel> Tankers { get; set; } = [];
        private List<string> BankNames { get; set; } = [];
        private MudTable<TankerViewModel>? Table { get; set; }

        protected override async Task OnInitializedAsync()
        {

            await this.BeginLoadingProcess();
            try
            {
                var res = await this.TankerService.GetAllTankersWithBankNames();

                if (res.Success)
                {
                    Tankers = res.Entities ?? [];
                }
                else
                {
                    this.HandleMessage(res.Message, res.Success);
                }

            }
            catch (Exception err)
            {
                this.HandleMessage(err.Message + Environment.NewLine + err.InnerException?.Message, false);
            }
            await this.EndLoadingProcess();
            await base.OnInitializedAsync();
        }


        private bool NeedsRender = true;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender && NeedsRender)
            {
                foreach (var item in this.Tankers)
                {
                    if (item.DriverBankId != null && item.DriverBankName == null)
                        item.DriverBankName = await GetBankName(item.DriverBankId);
                    if (item.OwnerBankId != null && item.OwnerBankName == null)
                        item.OwnerBankName = await GetBankName(item.OwnerBankId);
                }
                NeedsRender = false;
                this.StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task<string> GetBankName(Guid? bankId)
        {
            if (bankId != null)
            {
                var res = await this.BankService.GetBankById((Guid)bankId);
                if (res.Success)
                    return res?.Entities?.FirstOrDefault()?.Name ?? string.Empty;
                return string.Empty;
            }
            else return string.Empty;
        }

        private void GoToNewTanker()=> this.NavigationManager.NavigateTo($"/Tankers/AddNew");
        private void HandleClick(TableRowClickEventArgs<TankerViewModel> e)
        {
            this.NavigationManager.NavigateTo($"/Tankers/Edit/{e.Item?.Id}");
        }

    }
}
