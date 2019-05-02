using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using VocabularyPracticeWeb.Helpers.Constants;
using VocabularyPracticeWeb.Helpers.Validators;

namespace VocabularyPracticeWeb.Areas.Lessons.Models
{
	public class LessonViewModel
	{
		[Required]
		public string LessonName { get; set; }

		[Required]
		public string NativeLanguage { get; set; }

		[Required]
		public string LearnedLanguage { get; set; }

		[FileType(MimeTypes.Text)]
		public IFormFile File { get; set; }
	}
}