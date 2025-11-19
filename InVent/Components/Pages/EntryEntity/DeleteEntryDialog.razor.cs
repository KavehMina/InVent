using MudBlazor;

namespace InVent.Components.Pages.EntryEntity
{
    public partial class DeleteEntryDialog
    {
        private async Task Submit()
        {
            try
            {
                this.Entry.Product = null;
                this.Entry.Refinery = null;
                this.Entry.DeliveryOrder = null;
                this.Entry.Package = null;
                this.Entry.Tanker = null;
                var res = await this.EntryService.Delete(this.Entry);
                HandleMessage(res.Message, res.Success);
                if (res.Success)
                {
                    MudDialog?.Close(DialogResult.Ok(true));
                }
            }
            catch (Exception err)
            {
                HandleMessage("حذف ناموفق" + "\n" + err.Message, false);
            }

        }
    }
}
