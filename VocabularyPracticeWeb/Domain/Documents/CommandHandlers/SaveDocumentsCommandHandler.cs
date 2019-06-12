using Common.Cqrs;
using System.Collections.Generic;
using VocabularyPracticeWeb.Infrastructure;

namespace VocabularyPracticeWeb.Domain.Documents
{
	public class SaveDocumentsCommandHandler : ICommand<IEnumerable<Document>, SaveDocuments>
	{
		private IBlobRepository<Document> _repo;

		public SaveDocumentsCommandHandler(IBlobRepository<Document> repo)
		{
			_repo = repo;
		}

		public IEnumerable<Document> Execute(SaveDocuments command)
		{ 
			_repo.SaveCollection(command.Documents);

			return command.Documents;
		}
	}
}
