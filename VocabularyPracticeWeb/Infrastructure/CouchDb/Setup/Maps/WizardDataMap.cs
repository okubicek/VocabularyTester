using ArmChair.EntityManagement.Config;
using VocabularyPracticeWeb.Domain.Wizard;

namespace VocabularyPracticeWeb.Infrastructure.CouchDb.Setup
{
	public class WizardDataMap : ClassMap<Wizard>
	{
		public WizardDataMap()
		{
			Id(x => x.WizardId);
			Revision(x => x.RevId);
		}
	}
}
