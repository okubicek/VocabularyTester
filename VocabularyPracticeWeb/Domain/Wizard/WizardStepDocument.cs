namespace VocabularyPracticeWeb.Domain.Wizard
{
	public class WizardStepDocument
	{
		public WizardStepDocument()
		{
		}

		public WizardStepDocument(string fileName, string fileId, string hash)
		{
			FileName = fileName;
			FileId = fileId;
			Hash = hash;
		}

		public string FileName { get; private set; }

		public string FileId { get; private set; }

		public string Hash { get; private set; }
	}
}