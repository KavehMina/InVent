
using MudBlazor;

namespace InVent.Components.Pages.TankerEntity
{
    public partial class DeleteTankerDialog()
    {
        //[CascadingParameter]
        //private IMudDialogInstance? MudDialog { get; set; }
        //[Parameter]
        //public Tanker Tanker { get; set; } = new Tanker()
        //{
        //    CargoType = string.Empty,
        //    DriverName = string.Empty,
        //    DriverPhone = string.Empty,
        //    Number = string.Empty
        //};
        //[Parameter]
        //public string Header { get; set; } = string.Empty;
        //[Parameter]
        //public string Message { get; set; } = string.Empty;       


        private async Task Submit()
        {
            try
            {
                var res = await this.TankerService.Delete(this.Tanker.Id);
                this.HandleMessage(res.Message, res.Success);
            }
            catch (Exception err)
            {
                this.HandleMessage("حذف ناموفق" + "\n" + err.Message, false);
            }

            MudDialog?.Close(DialogResult.Ok(true));
        }
        //private void Cancel() => MudDialog?.Cancel();
    }
}
