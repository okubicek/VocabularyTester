using System.Collections.Generic;
using VocabularyPracticeDomain.Vocabulary;

namespace VocabularyPracticeApplication.Api
{
	public interface ILanguageRepository
	{
		ICollection<Language> GetLanguageCollection();
	}
}
