using Common.Cqrs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VocabularyPracticeWeb.Domain.Documents;
using VocabularyPracticeWeb.Domain.Wizard;
using VocabularyPracticeWeb.Helpers.Extensions;

namespace VocabularyPracticeWeb.Areas.Lessons.Controllers
{
	[Area("Lessons")]
	public class LessonWizardController : Controller
    {
		private IQuery<Wizard, GetWizard> _getWizard;

		private ICommand<Wizard, SaveWizardStep> _saveWizard;

		public LessonWizardController(
				IQuery<Wizard, GetWizard> getWizard, 
				ICommand<Wizard, SaveWizardStep> saveWizard)
		{
			_getWizard = getWizard;
			_saveWizard = saveWizard;
		}

		public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult Add(string wizardId)
		{
			var wizardStep =
				GetWizardStep<LessonViewModel>(wizardId, LessonWizard.WizardSteps.Add)
				?? new LessonViewModel();

			var model = new LessonViewPageModel
			{
				AvailableLanguages = GetAvailableLanguages().ToSelectListItems(x => x, x => x, x => false, false)
			};

			model.LearnedLanguage = wizardStep.LearnedLanguage;
			model.LessonName = wizardStep.LessonName;
			model.NativeLanguage = wizardStep.NativeLanguage;
			model.FileName = wizardStep.FileName;

			return View(model);
		}

		[HttpPost]
		public IActionResult Add(string wizardId, LessonViewModel model)
		{
			if (ModelState.IsValid)
			{ 				
				var docs = model.File != null ? new List<Document>
				{
					new Document(model.File.ToByteArray(), 
						model.File.FileName, 
						DocumentType.LessonVocabulary)
				} : null;

				var wizardStepData = WizardStepData.AttachData(model, docs);

				var wizard = _saveWizard.Execute(new SaveWizardStep {
					Data = wizardStepData,
					WizardId = wizardId,
					StepName = LessonWizard.WizardSteps.Add
				});

				wizardId = wizard.WizardId;
			}

			ModelState.Remove("WizardId");

			return View(new LessonViewPageModel
			{
				WizardId = wizardId,
				LearnedLanguage = model.LearnedLanguage,
				NativeLanguage = model.NativeLanguage,
				LessonName = model.LessonName,
				FileName = model.FileName,
				File = model.File,
				AvailableLanguages = GetAvailableLanguages().ToSelectListItems(x => x, x => x, x => false, false)
			});
		}

		public IEnumerable<string> GetAvailableLanguages()
		{
			return new List<string> {
				"Czech",
				"German",
				"Spanish"
			};
		}

		private T GetWizardStep<T>(string wizardId, string stepName) where T : class
		{
			if (wizardId == null)
			{
				return null;
			}

			var wizard = _getWizard.Get(new GetWizard(wizardId));
			var wizardStep = wizard?.GetStepData<T>(stepName);

			return wizardStep;
		}
	}
}