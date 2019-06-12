namespace VocabularyPracticeWeb.Domain.Wizard
{
	public class SaveWizardStep
	{
		public string WizardId { get; set; }

		public string StepName { get; set; }

		public WizardStepData Data { get; set; }

		public int SavedByUserId { get; set; }
	}
}
