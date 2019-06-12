using Common.Cqrs;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using VocabularyPracticeWeb.Domain.Documents;
using VocabularyPracticeWeb.Helpers.Extensions;
using VocabularyPracticeWeb.Infrastructure;

namespace VocabularyPracticeWeb.Domain.Wizard
{
	public class SaveWizardStepCommandHandler : ICommand<Wizard, SaveWizardStep>
	{
		private IBlobRepository<Wizard> _repo;

		private ICommand<IEnumerable<Document>, SaveDocuments> _saveDocuments;

		private IQuery<Wizard, GetWizard> _getWizard;

		private HashAlgorithm _hashAlg;

		public SaveWizardStepCommandHandler(IBlobRepository<Wizard> repo,
			HashAlgorithm hashAlg,
			ICommand<IEnumerable<Document>, SaveDocuments> saveDocuments,
			IQuery<Wizard, GetWizard> getWizard)
		{
			_repo = repo;
			_saveDocuments = saveDocuments;
			_hashAlg = hashAlg;
			_getWizard = getWizard;
		}

		public Wizard Execute(SaveWizardStep command)
		{
			var wizard = GetWizardInstance(command);

			wizard.AttachStepData(command.StepName, command.Data.Data);
			ProcessDocuments(command, wizard);

			_repo.Save(wizard);

			return wizard;
		}

		private Wizard GetWizardInstance(SaveWizardStep command)
		{
			var wizard = command.WizardId != null 
				? _getWizard.Get(new GetWizard(command.WizardId)) 
				: new Wizard(command.SavedByUserId);

			return wizard;
		}

		private void ProcessDocuments(SaveWizardStep command, Wizard wizard)
		{
			var existingWizardDocs = wizard.GetStepDocuments(command.StepName);
			var documentsToStore = command.Data.Files;

			if ((documentsToStore?.Any() ?? false))
			{
				CreateOrUpdateDocuments(documentsToStore, existingWizardDocs);
			}

			HandleDeletes(documentsToStore, existingWizardDocs);
		}

		private void CreateOrUpdateDocuments(IEnumerable<Document> docs, List<WizardStepDocument> existingWizardDocs)
		{
			if (NoDocsToStore(docs))
			{
				return;
			}

			var documentsToStore = new List<Document>();
			foreach (var d in docs)
			{
				var hash = _hashAlg.ComputeHashString(d.Content);

				if (existingWizardDocs.IsChanged(d.FileName, hash))
				{
					documentsToStore.Add(d);
				}
			}

			var documents = _saveDocuments.Execute(new SaveDocuments { Documents = documentsToStore });

			var wizardStepDoc = documents.Select(x => new WizardStepDocument(
															x.FileName,
															x.Id,
															_hashAlg.ComputeHashString(x.Content)));
			existingWizardDocs.AddOrUpdate(wizardStepDoc);
		}

		private static bool NoDocsToStore(IEnumerable<Document> docsToStore)
		{
			return !docsToStore.Any(x => x.Content != null && x.Content.Length > 0);
		}

		private static void HandleDeletes(IEnumerable<Document> docs, List<WizardStepDocument> wizardDocuments)
		{
			var toDelete = wizardDocuments.Where(wd =>
				docs == null ||
				!docs.Any(a => a.FileName.Equals(wd.FileName)));

			if (toDelete.Any())
			{
				wizardDocuments.RemoveDocs(toDelete);
			}
		}

		private static bool ExistingWithNoChange(byte[] hash, WizardStepDocument existing)
		{
			return (existing != null && existing.Hash.Equals(hash));
		}
	}
}
