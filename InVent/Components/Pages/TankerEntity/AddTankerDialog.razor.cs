using InVent.Data.Models;
using InVent.Services.BankServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Text.RegularExpressions;

namespace InVent.Components.Pages.TankerEntity
{
    public partial class AddTankerDialog
    {
        [Inject]
        public required BankService BankService { get; set; }
        const string RPO = "RPO";
        const string SlackWax = "Slack Wax";
        const string RPOSlack = "RPO و Slack Wax";
        const string Bitumen = "قیر";

        public required string First { get; set; }
        public required string Second { get; set; } = "ع";
        public required string Third { get; set; }
        public required string Fourth { get; set; }


        /// <NOTE START>
        /// the order is like this because the logical order e.i. (1st + 2nd + 3rd + 4th),
        /// results in incorrect text order caused by the persian letter in the middle of a text.
        //public string TankerNumber => Third + Fourth + Second + First;
        /// </NOTE END>
        public required string DriverName { get; set; }
        public required string DriverPhone { get; set; }
        public string? DriverBankNumber { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerPhone { get; set; }
        public string? OwnerBankNumber { get; set; }
        public required string CargoType { get; set; }

        public List<Bank> Banks { get; set; } = [];
        public Bank? DriverBank { get; set; }
        public Bank? OwnerBank { get; set; }

        protected override void OnInitialized()
        {
            this.SetState(new State<Tanker>());
            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var res = await BankService.GetAllBanks();
                if (res.Success)
                    this.Banks = res.Entities?.ToList() ?? [];
                else this.HandleMessage(res.Message, res.Success);
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
                await Submit();
        }

        public async Task Submit()
        {
            await form.Validate();
            await this.BeginLoadingProcess();
            if (form.IsValid)
            {
                var newTanker = new TankerDTO
                {
                    Number = this.State.Value.Number,
                    //Number = this.TankerNumber,
                    DriverName = this.DriverName,
                    DriverPhone = this.DriverPhone,
                    DriverBankNumber = this.DriverBankNumber,
                    DriverBankId = this.DriverBank?.Id,
                    OwnerName = this.OwnerName,
                    OwnerPhone = this.OwnerPhone,
                    OwnerBankNumber = this.OwnerBankNumber,
                    OwnerBankId = this.OwnerBank?.Id,
                    CargoType = this.CargoType,
                };

                try
                {
                    var res = await TankerService.Add(newTanker);
                    this.HandleMessage(res.Message, res.Success);

                    if (res.Success)
                        await form.ResetAsync();
                }
                catch (Exception err)
                {
                    this.HandleMessage(err.Message + Environment.NewLine + err.InnerException?.Message, false);
                }

            }
            else
            {
                this.HandleMessage("Invalid Form", false);
            }

            await this.EndLoadingProcess();
        }

        private async Task MoveFocus(string value, MudTextField<string> thisField, MudTextField<string> nextField)
        {

            if (thisField != null && nextField != null)
            {
                switch (thisField.InputId)
                {
                    case "1":
                        First = value;
                        if (thisField?.Value?.Length == 2)
                            await nextField.FocusAsync();
                        break;
                    case "2":
                        Second = value;
                        if (thisField?.Value?.Length == 1)
                            await nextField.FocusAsync();
                        break;
                    case "3":
                        Third = value;
                        if (thisField?.Value?.Length == 3)
                            await nextField.FocusAsync();
                        break;
                    case "4":
                        Fourth = value;
                        if (thisField?.Value?.Length == 2)
                            await nextField.FocusAsync();
                        break;
                    case "6":
                        DriverPhone = value;
                        if (thisField?.Value?.Length == 11)
                            await nextField.FocusAsync();
                        break;
                    case "9":
                        OwnerPhone = value;
                        if (thisField?.Value?.Length == 11)
                            await nextField.FocusAsync();
                        break;
                    default:
                        break;
                }
            }

        }


        private List<MudTextField<string>> TextFieldRefs = new(new MudTextField<string>[12]);

        public PatternMask Mask1 = new("00");
        public PatternMask Mask2 = new("a");
        public PatternMask Mask3 = new("000");
        public PatternMask MobileMask = new("00000000000");

        private string ValidateMobilePhone(string arg)
        {
            if (arg != null && arg != string.Empty)
            {
                Regex regex = new Regex("09\\d\\d\\d\\d\\d\\d\\d\\d\\d", RegexOptions.IgnoreCase);
                if (!regex.IsMatch(arg))
                    return "موبایل نامعتبر";
            }
            return string.Empty;
        }
        private string ValidateFirstPartofNumberPlate(string arg)
        {
            if (arg?.Length < 2)
                return "پلاک نامعتبر";
            return string.Empty;
        }
        private string ValidateSecondPartofNumberPlate(string arg)
        {
            if (arg?.Length < 1)
                return "پلاک نامعتبر";
            return string.Empty;
        }
        private string ValidateThirdPartofNumberPlate(string arg)
        {
            if (arg?.Length < 3)
                return "پلاک نامعتبر";
            return string.Empty;
        }
        private string ValidateForthPartofNumberPlate(string arg)
        {
            if (arg?.Length < 2)
                return "پلاک نامعتبر";
            return string.Empty;
        }


    }
}
