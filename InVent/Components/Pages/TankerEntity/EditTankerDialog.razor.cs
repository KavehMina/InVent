using InVent.Data.Models;
using InVent.Services.BankServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Text.RegularExpressions;

namespace InVent.Components.Pages.TankerEntity
{
    public partial class EditTankerDialog
    {
        [Inject]
        public required BankService BankService { get; set; }
        //[Parameter]
        //public string TankerId { get; set; } = string.Empty;
        //private Tanker Tanker { get; set; } = new Tanker()
        //{
        //    CargoType = string.Empty,
        //    DriverName = string.Empty,
        //    DriverPhone = string.Empty,
        //    Number = string.Empty
        //};

        const string RPO = "RPO";
        const string SlackWax = "Slack Wax";
        const string RPOSlack = "RPO و Slack Wax";
        const string Bitumen = "قیر";

        public required string First { get; set; }
        public required string Second { get; set; }
        public required string Third { get; set; }
        public required string Fourth { get; set; }

        /// <NOTE START>
        /// the order is like this because the logical order e.i. (1st + 2nd + 3rd + 4th),
        /// results in incorrect text order caused by the persian letter in the middle of a text.
        public string TankerNumber => Third + Fourth + Second + First;
        /// </NOTE END>

        //public new bool ButtonDisabled => !form.IsValid || !form.IsTouched;
        public List<Bank> Banks { get; set; } = [];
        //public Bank? DriverBank { get; set; }
        //public Bank? OwnerBank { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                //var res = await this.TankerService.GetById(this.Tanker.Id);
                //if (res.Success)
                //{
                //    Tanker = res.Entities?.FirstOrDefault() ?? Tanker;
                //    SplitTankerNumber();
                //}
                //else this.HandleMessage(res.Message, res.Success);
                Banks = (await BankService.GetAllBanks()).Entities ?? [];
            }
            catch (Exception err)
            {
                this.HandleMessage(err.Message + Environment.NewLine + err.InnerException?.Message, false);
            }
            await base.OnInitializedAsync();
        }

        protected override Task OnParametersSetAsync()
        {
            SplitTankerNumber();
            return base.OnParametersSetAsync();
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

        private void SplitTankerNumber()
        {
            /// <NOTE START>
            /// the order is like this because the logical order e.i. (1st + 2nd + 3rd + 4th),
            /// results in incorrect text order caused by the persian letter in the middle of a text.
            Third = Tanker.Number?.Substring(0, 3) ?? "";
            Fourth = Tanker.Number?.Substring(3, 2) ?? "";
            Second = Tanker.Number?.Substring(5, 1) ?? "";
            First = Tanker.Number?.Substring(6, 2) ?? "";
            /// </NOTE END>
        }

        private async Task DetectEnter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
                await Submit();
        }

        private async Task Submit()
        {
            await this.form.Validate();
            await this.BeginLoadingProcess();

            if (this.form.IsValid)
            {
                try
                {
                    Tanker.Number = TankerNumber;
                    Tanker.DriverBankId = Tanker.DriverBank?.Id;
                    Tanker.OwnerBankId = Tanker.OwnerBank?.Id;

                    var tempTanker = new TankerDTO
                    {
                        Id = this.Tanker.Id,
                        Number = this.TankerNumber,//this.Tanker.Number,
                        DriverName = this.Tanker.DriverName,
                        DriverPhone = this.Tanker.DriverPhone,
                        DriverBankNumber = this.Tanker.DriverBankNumber,
                        DriverBankId = this.Tanker.DriverBank?.Id,
                        OwnerName = this.Tanker.OwnerName,
                        OwnerPhone = this.Tanker.OwnerPhone,
                        OwnerBankNumber = this.Tanker.OwnerBankNumber,
                        OwnerBankId = this.Tanker.OwnerBank?.Id,
                        CargoType = this.Tanker.CargoType,
                    };

                    var res = await this.TankerService.Update(tempTanker);
                    this.HandleMessage(res.Message, res.Success);
                    MudDialog?.Close(DialogResult.Ok(true));

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

            if (thisField != null)
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

        private static string ValidateMobilePhone(string arg)
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
            if (arg.Length < 2)
                return "پلاک نامعتبر";
            return string.Empty;
        }
        private string ValidateSecondPartofNumberPlate(string arg)
        {
            if (arg.Length < 1)
                return "پلاک نامعتبر";
            return string.Empty;
        }
        private string ValidateThirdPartofNumberPlate(string arg)
        {
            if (arg.Length < 3)
                return "پلاک نامعتبر";
            return string.Empty;
        }
        private string ValidateForthPartofNumberPlate(string arg)
        {
            if (arg.Length < 2)
                return "پلاک نامعتبر";
            return string.Empty;
        }
    }
}
