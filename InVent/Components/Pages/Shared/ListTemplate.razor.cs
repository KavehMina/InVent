using Microsoft.AspNetCore.Components;

namespace InVent.Components.Pages.Shared
{
    public partial class ListTemplate<T>
    {
        [Parameter]
        public RenderFragment? TableHeader { get; set; }

        [Parameter, EditorRequired]
        public RenderFragment<T> RowTemplate { get; set; } = default!;

        [Parameter, EditorRequired]
        public IReadOnlyList<T> Items { get; set; } = default!;
    }
}
