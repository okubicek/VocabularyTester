using Common.Cqrs;
using VocabularyPracticeWeb.Infrastructure;

namespace VocabularyPracticeWeb.Domain.Wizard
{
	public class GetWizardQueryHandler : IQuery<Wizard, GetWizard>
	{
		private IBlobRepository<Wizard> _repo;

		public GetWizardQueryHandler(IBlobRepository<Wizard> repo)
		{
			_repo = repo;
		}

		public Wizard Get(GetWizard query)
		{
			return _repo.GetById(query.Id);
		}
	}
}