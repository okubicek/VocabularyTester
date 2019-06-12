using Newtonsoft.Json;
using System.Collections.Generic;
using VocabularyPracticeWeb.Domain.Documents;
using VocabularyPracticeWeb.Infrastructure.JsonSerialization;

namespace VocabularyPracticeWeb.Domain.Wizard
{
	public class WizardStepData
	{
		public string Data { get; private set; }

		public IEnumerable<Document> Files { get; private set; }

		public void AttachData<T>(T data, IEnumerable<Document> files)
		{
			var settings = GetJsonSettings();
			Data = JsonConvert.SerializeObject(data, settings);
			Files = files;
		}

		private static JsonSerializerSettings GetJsonSettings()
		{
			var settings = new JsonSerializerSettings();
			settings.ContractResolver = new IgnoreBlacklistedPropertiesContractResolver();
			return settings;
		}
	}
}
