using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VocabularyPracticeWeb.Helpers.Extensions
{
    public static class IEnumerableExtensions
    {
		public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, Func<T,string> getText, Func<T, string> getValue, Func<T, bool> isSelected, bool includeEmpty)
		{
			var list = new List<SelectListItem>();

			if (includeEmpty)
			{
				list.Add(new SelectListItem
				{
					Text = string.Empty,
					Value = string.Empty
				});
			}

			list.AddRange(items.Select(x => new SelectListItem
			{
				Text = getText(x),
				Value = getValue(x),
				Selected = isSelected(x)
			}));

			return list;
		}
    }
}
