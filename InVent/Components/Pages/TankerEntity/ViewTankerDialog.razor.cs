using InVent.Components.Pages.BankEntity;
using InVent.Services.BankServices;

namespace InVent.Components.Pages.TankerEntity
{
    public partial class ViewTankerDialog
    {
        public required string First { get; set; }
        public required string Second { get; set; }
        public required string Third { get; set; }
        public required string Fourth { get; set; }

        protected override Task OnParametersSetAsync()
        {
            SplitTankerNumber();
            return base.OnParametersSetAsync();
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
    }
}
