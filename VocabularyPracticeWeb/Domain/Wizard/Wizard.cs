using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace VocabularyPracticeWeb.Domain.Wizard
{
	public class Wizard
	{
		public string WizardId { get; set; }

		public string RevId { get; set; }

		public int UserId { get; private set; }

		private List<WizardStep> _steps = new List<WizardStep>();

		protected Wizard()
		{
		}

		public Wizard(int userId)
		{
			UserId = userId;
		}

		public void AttachStepData(string stepName, string jsonData)
		{		
			var step = GetStep(stepName);
			if (step != null)
			{
				step.UpdateData(jsonData);
			}
			else
			{
				_steps.Add(new WizardStep(stepName, jsonData));
			}
		}

		public T GetStepData<T>(string stepName) where T : class
		{
			var data = GetStep(stepName);
			return data == null ? null : JsonConvert.DeserializeObject<T>(data.Data);
		}

		private WizardStep GetStep(string stepName)
		{
			return _steps.FirstOrDefault(x => x.StepName.Equals(stepName));
		}

		public List<WizardStepDocument> GetStepDocuments(string stepName)
		{
			var data = _steps.FirstOrDefault(x => x.StepName.Equals(stepName));
			return data.Files;
		}

		private class WizardStep
		{
			protected WizardStep()
			{
			}

			public WizardStep(string stepName, string data)
			{
				StepName = stepName;
				Data = data;
				Files = new List<WizardStepDocument>();
			}

			public string StepName { get; private set; }

			public string Data { get; private set; }

			public List<WizardStepDocument> Files { get; private set; }

			public void UpdateData(string seriallisedData)
			{
				this.Data = seriallisedData;
			}
		}
	}
}
