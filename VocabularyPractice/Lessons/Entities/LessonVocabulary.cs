using VocabularyPracticeDomain.Vocabulary;

namespace VocabularyPracticeDomain.Lessons
{
	public class LessonVocabulary
	{
		private LessonVocabulary()
		{
		}

		public LessonVocabulary(Lesson lesson, Word nativeWord, Word foreignWord)
		{
			Lesson = lesson;
			NativeWord = nativeWord;
			ForeignWord = foreignWord;
		}

		public Lesson Lesson { get; set; }

		public Word NativeWord { get; set; }

		public Word ForeignWord { get; set; }
	}
}
