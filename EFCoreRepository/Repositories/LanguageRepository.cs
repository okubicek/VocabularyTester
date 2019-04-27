using System.Collections.Generic;
using System.Linq;
using VocabularyPracticeApplication.Api;
using VocabularyPracticeDomain.Vocabulary;

namespace VocabularyPracticeEFCoreRepository.Repositories
{
	public class LanguageRepository : ILanguageRepository
	{
		private VocabularyPracticeDbContext _context;

		public LanguageRepository(VocabularyPracticeDbContext context)
		{
			_context = context;
		}
		
		public ICollection<Language> GetLanguageCollection()
		{
			return _context.Language.ToList();
		}
	}
}
