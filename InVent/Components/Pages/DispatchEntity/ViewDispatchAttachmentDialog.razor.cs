using InVent.Data.Models;
using Microsoft.AspNetCore.Components;

namespace InVent.Components.Pages.DispatchEntity
{
    public partial class ViewDispatchAttachmentDialog
    {
        [Parameter]
        public required Attachment Attachment { get; set; }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }

    }
}
