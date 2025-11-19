namespace InVent.Components.Pages.DeliveryOrderEntity
{
    public partial class ViewDeliveryOrderDialog
    {
        private string StatusText => this.DeliveryOrder.Status == true ? "بسته" : "باز";

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }
    }
}
