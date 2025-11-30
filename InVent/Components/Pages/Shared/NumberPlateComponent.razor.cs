using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InVent.Components.Pages.Shared
{
    public partial class NumberPlateComponent
    {
        //[Parameter]
        //public required string TankerNumber { get; set; }
        [Parameter]
        public required List<MudTextField<string>> TextFieldRefs {  get; set; }

        public PatternMask Mask1 = new("00");
        public PatternMask Mask2 = new("a");
        public PatternMask Mask3 = new("000");
        [Parameter]
        public required string First { get; set; }
        [Parameter]
        public required string Second { get; set; }
        [Parameter]
        public required string Third { get; set; }
        [Parameter]
        public required string Fourth { get; set; }

                

        private async Task MoveFocus(string value, MudTextField<string> thisField, MudTextField<string> nextField)
        {

            if (thisField != null && nextField != null)
            {
                switch (thisField.InputId)
                {
                    case "1":
                        First = value;
                        //this.TankerNumber = Third + Fourth + Second + First;
                        if (thisField?.Value?.Length == 2)
                            await nextField.FocusAsync();
                        break;
                    case "2":
                        Second = value;
                        //this.TankerNumber = Third + Fourth + Second + First;
                        if (thisField?.Value?.Length == 1)
                            await nextField.FocusAsync();
                        break;
                    case "3":
                        Third = value;
                        //this.TankerNumber = Third + Fourth + Second + First;
                        if (thisField?.Value?.Length == 3)
                            await nextField.FocusAsync();
                        break;
                    case "4":
                        Fourth = value;
                        //this.TankerNumber = Third + Fourth + Second + First;
                        if (thisField?.Value?.Length == 2)
                            await nextField.FocusAsync();
                        break;
                    default:
                        break;
                }
            }

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
