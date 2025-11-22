using MudBlazor;

namespace InVent.Components.Pages.BookingEntity
{
    public partial class DeleteBookingDialog
    {
        private async Task Submit()
        {
            try
            {

                //this.Booking.Project = null;
                var res = await this.BookingService.Delete(this.Booking.Id);
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
