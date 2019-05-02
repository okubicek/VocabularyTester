using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VocabularyPracticeWeb.Areas.Lessons.Models
{
	public class LessonViewPageModel : LessonViewModel
	{
		public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
	}
}
