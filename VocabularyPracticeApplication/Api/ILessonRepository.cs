using VocabularyPracticeDomain.Lessons;

namespace VocabularyPracticeApplication.Api
{
	public interface ILessonRepository
	{
		void Add(Lesson lesson);
	}
}
