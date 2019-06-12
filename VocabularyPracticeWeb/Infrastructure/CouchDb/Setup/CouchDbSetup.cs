using ArmChair;
using ArmChair.EntityManagement.Config;
using ArmChair.Http;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace VocabularyPracticeWeb.Infrastructure.CouchDb.Setup
{
	public class CouchDbSetup
	{
		public void SetupDatabase(Database x)
		{
			x.Register(new List<ClassMap>{
				new WizardDataMap(),
				new DocumentMap()
			});
		}

		public void EnsureDatabaseExists(IConnection conn, string dbName)
		{
			var checkDbExists = GenerateRequest(dbName);
			using (var response = conn.Execute(checkDbExists))
			{
				if (response.Status != HttpStatusCode.NotFound)
					return;
			}

			var createDb = GenerateRequest(dbName, HttpMethod.Put);
			using (var response = conn.Execute(createDb))
			{
				if (response.Status != HttpStatusCode.Created)
				{
					throw new System.Exception("Could not create WizardData db in couch DB");
				}
			}
		}

		private Request GenerateRequest(string dbName, HttpMethod method = null)
		{
			var dbRequest = new Request("/:db", method);
			dbRequest.AddUrlSegment("db", dbName);
			dbRequest.SetContentType(ContentType.Json);

			return dbRequest;
		}
	}
}
