using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VocabularyPracticeWeb.Areas.Lessons
{
	public class LessonViewPageModel : LessonViewModel
	{
		public string WizardId { get; set; }

		public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
	}
}
