using VocabularyPracticeApplication.Api;
using VocabularyPracticeDomain.Lessons;

namespace VocabularyPracticeEFCoreRepository.Repositories
{
	public class LessonRepository : ILessonRepository
	{
		private VocabularyPracticeDbContext _context;

		public LessonRepository(VocabularyPracticeDbContext context)
		{
			_context = context;
		}

		public void Add(Lesson lesson)
		{
			_context.Add(lesson);
			_context.SaveChanges();
		}
	}
}
