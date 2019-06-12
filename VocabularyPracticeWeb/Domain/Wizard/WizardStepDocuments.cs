using System.Collections.Generic;
using System.Linq;

namespace VocabularyPracticeWeb.Domain.Wizard
{
	public static class WizardStepDocumentExtensions
	{
		public static bool IsChanged(this List<WizardStepDocument> wst, string fileName, string hash)
		{
			var existing = wst.SingleOrDefault(x => x.FileName.Equals(fileName));

			return !NoChangeRequired(hash, existing);
		}

		private static bool NoChangeRequired(string hash, WizardStepDocument existing)
		{
			return (existing != null && existing.Hash.Equals(hash));
		}

		public static void AddOrUpdate(this List<WizardStepDocument> wst, IEnumerable<WizardStepDocument> documents)
		{
			foreach(var d in documents)
			{
				var existing = wst.FirstOrDefault(x => x.FileName.Equals(d.FileName));
				if (existing != null)
				{
					wst.Remove(existing);
				}

				wst.Add(d);
			}
		}

		public static void RemoveDocs(this List<WizardStepDocument> wst, IEnumerable<WizardStepDocument> docsToRemove)
		{
			foreach(var doc in docsToRemove.ToList())
			{
				wst.Remove(doc);
			}			
		}
	}
}
