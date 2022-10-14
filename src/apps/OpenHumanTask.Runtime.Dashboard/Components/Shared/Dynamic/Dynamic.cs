using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace OpenHumanTask.Runtime.Dashboard.Components;

public class Dynamic 
    : ComponentBase
{

    [Parameter]
    public string Tag { get; set; } = null!;

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public Dictionary<string, object>? Attributes { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (string.IsNullOrWhiteSpace(this.Tag)) throw new ArgumentNullException(nameof(this.Tag));
        builder.OpenElement(0, this.Tag);
        if (!string.IsNullOrWhiteSpace(this.Class))
            builder.AddAttribute(1, "class", this.Class);
        if (this.Attributes?.Any() == true)
            builder.AddMultipleAttributes(2, this.Attributes);
        if (this.ChildContent != null)
            builder.AddContent(3, this.ChildContent);
        builder.CloseElement();
    }

}