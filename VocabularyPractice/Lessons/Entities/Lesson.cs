namespace VocabularyPracticeDomain.Lessons
{
	public class Lesson : EntityBase
	{
		public string Description { get; set; }

		public LessonVocabulary Vocabulary { get; set; }
    }
}