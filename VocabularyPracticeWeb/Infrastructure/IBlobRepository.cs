using System.Collections.Generic;

namespace VocabularyPracticeWeb.Infrastructure
{
	public interface IBlobRepository<T> where T : class
	{
		T GetById(string id);

		void Save(T data);

		void SaveCollection(IEnumerable<T> data);
	}
}