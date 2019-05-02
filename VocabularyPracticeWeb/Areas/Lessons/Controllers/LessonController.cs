using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VocabularyPracticeWeb.Areas.Lessons.Models;
using VocabularyPracticeWeb.Helpers.Extensions;

namespace VocabularyPracticeWeb.Areas.Lessons.Controllers
{
	[Area("Lessons")]
	public class LessonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult Add()
		{
			var model = new LessonViewPageModel
			{
				AvailableLanguages = GetAvailableLanguages().ToSelectListItems(x => x, x => x, x => false, false)
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Add(LessonViewModel model)
		{
			if (ModelState.IsValid)
			{
				//save data
				//redirect to next step
			}

			return View(new LessonViewPageModel
			{
				LearnedLanguage = model.LearnedLanguage,
				NativeLanguage = model.NativeLanguage,
				LessonName = model.LessonName,
				File = model.File,
				AvailableLanguages = GetAvailableLanguages().ToSelectListItems(x => x, x => x, x => false, false)
			});
		}

		public IEnumerable<string> GetAvailableLanguages()
		{
			return new List<string> {
				"Czech",
				"German",
				"Spanish"
			};
		}
    }
}