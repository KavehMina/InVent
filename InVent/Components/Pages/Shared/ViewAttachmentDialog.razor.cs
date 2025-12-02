using InVent.Data.Models;
using Microsoft.AspNetCore.Components;

namespace InVent.Components.Pages.Shared
{
    public partial class ViewAttachmentDialog
    {
        [Parameter]
        public required Attachment Attachment { get; set; }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }

    }
}
