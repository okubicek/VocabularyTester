using System.Collections.Generic;
using VocabularyPracticeDomain.Vocabulary;

namespace VocabularyPracticeDomain.Lessons
{
	public class Lesson : EntityBase
	{
		private Lesson()
		{
		}

		public Lesson(string description, Language nativeLanguage, Language foreignLanguage, ICollection<LessonVocabulary> lessonVocabulary)
		{
			Description = description;
			NativeLanguage = nativeLanguage;
			ForeignLanguage = foreignLanguage;
			LessonVocabulary = lessonVocabulary;
		}

		public string Description { get; set; }

		public Language NativeLanguage { get; set; }

		public Language ForeignLanguage { get; set; }

		public ICollection<LessonVocabulary> LessonVocabulary { get; set; }
	}
}