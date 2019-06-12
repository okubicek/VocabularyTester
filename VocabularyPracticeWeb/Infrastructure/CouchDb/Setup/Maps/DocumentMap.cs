using ArmChair.EntityManagement.Config;
using VocabularyPracticeWeb.Domain.Documents;

namespace VocabularyPracticeWeb.Infrastructure.CouchDb.Setup
{
	public class DocumentMap : ClassMap<Document>
	{
		public DocumentMap()
		{
			Id(x => x.Id);
			Revision(x => x.RevId);
		}
	}
}
