using System;
using System.Collections.Generic;
using VocabularyPracticeDomain.Vocabulary;

namespace VocabularyPracticeDomain.Lessons
{
	public class Lesson : EntityBase
	{
		private Lesson()
		{
		}

		public Lesson(string description, Language nativeLanguage, Language foreignLanguage)
		{
			Description = description;
			NativeLanguage = nativeLanguage;
			ForeignLanguage = foreignLanguage;
			LessonVocabulary = new List<LessonVocabulary>();
		}

		public string Description { get; private set; }

		public Language NativeLanguage { get; private set; }

		public Language ForeignLanguage { get; private set; }

		public ICollection<LessonVocabulary> LessonVocabulary { get; private set; }

		public void AddVocabulary(Word native, Word foreign)
		{
			if (native.Language != NativeLanguage || foreign.Language != ForeignLanguage)
			{
				throw new ApplicationException("Vocabulary language is not consistent with Lesson language settings");
			}

			LessonVocabulary.Add(
					new LessonVocabulary(this, native, foreign)
			);
		}
	}
}