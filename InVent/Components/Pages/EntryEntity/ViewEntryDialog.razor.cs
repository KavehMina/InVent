using MudBlazor;

namespace InVent.Components.Pages.EntryEntity
{
    public partial class ViewEntryDialog
    {
        private int? RefineryNet => this.Entry.RefineryFilled - this.Entry.RefineryEmpty;
        private int? WarehouseNet => this.Entry.WarehouseFilled - this.Entry.WarehouseEmpty;
        private DateTime? Date =>this.Entry.Date;
        private int? Difference => this.WarehouseNet - this.RefineryNet;
        private Double? Average => (double)this.WarehouseNet / this.Entry.Filled;
    }
}
