using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace VocabularyPracticeWeb.TagHelpers.Shared
{
	[HtmlTargetElement("info-message")]
    public class InfoMessageTagHelper : TagHelper
    {
		[ViewContext]
		public ViewContext ViewContext { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagMode = TagMode.SelfClosing;
			output.TagName = null;
			var currentClass = output.Attributes.ContainsName("class") ? output.Attributes["class"].Value.ToString() : string.Empty;

			if (ViewContext.ViewData.ContainsKey("info-message"))
			{
				var html = $"<div class=\"alert alert-info {currentClass}\">{(string)ViewContext.ViewData["info-message"]}</div>";
				output.Content.SetHtmlContent(html);
			}
		}
	}
}
