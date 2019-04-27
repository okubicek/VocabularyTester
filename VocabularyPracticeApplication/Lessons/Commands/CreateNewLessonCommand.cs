using System.Collections.Generic;

namespace VocabularyPracticeApplication.Lessons
{
	public class CreateNewLessonCommand
	{
		public int NativeLanguageId { get; set; }

		public int LearnedLanguageId { get; set; }

		public string Description { get; set; }

		public ICollection<Vocabulary> Vocabulary { get; set; }
	}
}
