using InVent.Data.Models;
using Microsoft.AspNetCore.Components;

namespace InVent.Components.Pages.DispatchEntity
{
    public partial class ViewDispatchAttachmentDialog
    {
        [Parameter]
        public required Attachment Attachment { get; set; }

        //private string ImageSource { get; set; } = string.Empty;

        protected override Task OnParametersSetAsync()
        {
            //this.ImageSource = $"{this.Attachment.FilePath}";
            //var base64 = Convert.ToBase64String(this.Attachment.FileData);
            //this.ImageSource = $"data:{Attachment.ContentType};base64,{base64}";
            return base.OnParametersSetAsync();
        }

    }
}
