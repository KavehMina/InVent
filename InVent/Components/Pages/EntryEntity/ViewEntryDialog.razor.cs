using InVent.Data.Models;
using InVent.Services.AttachmentServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace InVent.Components.Pages.EntryEntity
{
    public partial class ViewEntryDialog
    {
        [Inject]
        public required AttachmentService AttachmentService { get; set; }
        private int? RefineryNet => this.Entry.RefineryFilled - this.Entry.RefineryEmpty;
        private int? WarehouseNet => this.Entry.WarehouseFilled - this.Entry.WarehouseEmpty;
        private DateTime? Date =>this.Entry.Date;
        private int? Difference => this.WarehouseNet - this.RefineryNet;
        private Double? Average => (double)this.WarehouseNet / (this.Entry.Filled + this.Entry.Damaged);

        private readonly IList<IBrowserFile> files = [];

        private List<Attachment> Attachments = [];
        private List<Attachment> ExistingAttachments { get; set; } = [];

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.ExistingAttachments = (await AttachmentService.GetAll(this.Entry.Id, "entry")).Entities ?? [];
            }
            catch (Exception err)
            {
                HandleMessage(err.Message, false);
            }
            await base.OnInitializedAsync();
        }

        private string SetDifferenceColor()
        {
            switch (this.Difference)
            {
                case > 0:
                    return "background-color:palegreen";
                case < 0:
                    return "background-color:mistyrose";
                default:
                    return string.Empty;
            }
        }
    }
}
