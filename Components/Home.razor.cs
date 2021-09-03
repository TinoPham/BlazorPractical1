using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorProducts.Client.Components
{
	public partial class Home
	{
		[Parameter]
		public string Title { get; set; }

		[Parameter(CaptureUnmatchedValues = true)]
		public Dictionary<string, object> ImageAttributes { get; set; }

		[CascadingParameter(Name = "HeadingColor")]
		public string TitleColor { get; set; }

		[Parameter]
		public RenderFragment VisitShopContent { get; set; }
	}
}
