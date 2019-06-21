using System.Collections.Generic;
using VocabularyPracticeWeb.Domain.Documents;
using VocabularyPracticeWeb.Infrastructure.JsonSerialization;

namespace VocabularyPracticeWeb.Domain.Wizard
{
	public class WizardStepData
	{
		private WizardStepData(string data, IEnumerable<Document> files)
		{
			Data = data;
			Files = files;
		}

		public string Data { get; private set; }

		public IEnumerable<Document> Files { get; private set; }

		public static WizardStepData AttachData<T>(T data, IEnumerable<Document> files)
		{
			return new WizardStepData(JsonConverter.ConvertToJson(data), files);
		}
	}
}
