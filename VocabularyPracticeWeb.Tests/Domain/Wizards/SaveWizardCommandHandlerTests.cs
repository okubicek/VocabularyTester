using Common.Cqrs;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using VocabularyPracticeWeb.Domain.Documents;
using VocabularyPracticeWeb.Domain.Wizard;
using VocabularyPracticeWeb.Helpers.Extensions;
using VocabularyPracticeWeb.Infrastructure;
using Xunit;

namespace VocabularyPracticeWeb.Tests.Domain.Wizards
{
	public abstract class SaveWizardCommandHandlerTests
	{
		protected SaveWizardStepCommandHandler _underTest;

		protected const string Step1 = nameof(Step1);
		protected const string Step2 = nameof(Step2);

		protected HashAlgorithm HashAlgorithm;

		protected DummyData StepData = new DummyData
		{
			Age = 28,
			Name = "MyName"
		};

		protected byte[] DocumentContent = Convert.FromBase64String("aaaaaaaaaaaaaaaaaaaaaSaveaaaaaaaaaaaaaaaaaaTestaaaaaaaaaaaaaaaaaaaaa");
		protected string FileName = "file1";

		public SaveWizardCommandHandlerTests()
		{
			var wizardRepo = GenerateWizardRepoStub();
			var saveDocuments = GenerateSaveDocumentsStub();
			var wizardQuery = GenerateGetWizardStub();

			HashAlgorithm = new SHA1CryptoServiceProvider();
			_underTest = new SaveWizardStepCommandHandler(wizardRepo, HashAlgorithm, saveDocuments, wizardQuery);

			var stepData = new WizardStepData();
			stepData.AttachData(StepData, Documents);

			Wizard = _underTest.Execute(new SaveWizardStep
			{
				StepName = Step1,
				Data = stepData,
				SavedByUserId = 111,
				WizardId = WizardId
			});
		}

		protected abstract List<Document> Documents { get; }

		protected abstract Wizard Wizard { get; set; }

		protected abstract string WizardId { get; }

		private static IBlobRepository<Wizard> GenerateWizardRepoStub()
		{
			var wizardRepo = new Mock<IBlobRepository<Wizard>>();
			wizardRepo.Setup(x => x.Save(It.IsAny<Wizard>()));
			return wizardRepo.Object;
		}

		private static ICommand<IEnumerable<Document>, SaveDocuments> GenerateSaveDocumentsStub()
		{
			var saveDocuments = new Mock<ICommand<IEnumerable<Document>, SaveDocuments>>();
			saveDocuments.Setup(x => x.Execute(It.IsAny<SaveDocuments>())).Returns(
				(SaveDocuments sw) =>
				{
					var cnt = 1;
					foreach (var d in sw.Documents)
					{
						d.Id = cnt.ToString();
						cnt++;
					}

					return sw.Documents;
				});

			return saveDocuments.Object;
		}

		private IQuery<Wizard, GetWizard> GenerateGetWizardStub()
		{
			var getWizard = new Mock<IQuery<Wizard, GetWizard>>();
			getWizard.Setup(x => x.Get(It.IsAny<GetWizard>())).Returns(Wizard);

			return getWizard.Object;
		}

		protected class DummyData
		{
			public int Age { get; set; }

			public string Name { get; set; }
		}
	}

	public class WhenSavingNewWizardWithAttachedDocuments : SaveWizardCommandHandlerTests
	{
		private Wizard _w;

		protected override string WizardId => null;

		protected override List<Document> Documents
		{
			get
			{
				return new List<Document> { new Document(DocumentContent, FileName, DocumentType.LessonVocabulary) };
			}
		}

		protected override Wizard Wizard
		{
			get
			{
				if (_w == null)
				{
					_w = new Wizard(1);
				}
				return _w;
			}
			set
			{
				_w = value;
			}
		}

		[Fact]
		public void WizardContainsExpectedNumberOfDocument()
		{
			Assert.Single(Wizard.GetStepDocuments(Step1));
		}

		[Fact]
		public void WizardDocumentHasExpectedProperties()
		{
			var document = Wizard.GetStepDocuments(Step1);
			Assert.Equal(FileName, document.Single().FileName);
			Assert.Equal(HashAlgorithm.ComputeHashString(DocumentContent), document.Single().Hash);
		}

		[Fact]
		public void WizardDataHaveExpectedProperties()
		{
			var d = Wizard.GetStepData<DummyData>(Step1);
			Assert.Equal(StepData.Name, d.Name);
			Assert.Equal(StepData.Age, d.Age);
		}
	}

	public class WhenSavingWizardStepAfterRemovingDocuments : SaveWizardCommandHandlerTests
	{
		private Wizard _w;

		protected override List<Document> Documents => null;

		protected override string WizardId => "5";

		protected override Wizard Wizard
		{
			get
			{
				if (_w == null)
				{
					_w = new Wizard(1);
					_w.AttachStepData(Step1, JsonConvert.SerializeObject(StepData));
					_w.GetStepDocuments(Step1).AddOrUpdate(
						new List<WizardStepDocument>
						{
							new WizardStepDocument(FileName, "12", "1111111")
						}
					);
				}

				return _w;
			}
			set
			{
				_w = value;
			}
		}

		[Fact]
		public void WizardContainsNoDocuments()
		{
			Assert.Empty(Wizard.GetStepDocuments(Step1));
		}
	}

	public class WhenSavingWizardWithUpdatedDocument : SaveWizardCommandHandlerTests
	{
		private Wizard _w;

		protected override string WizardId => "5";

		protected override List<Document> Documents
		{
			get
			{
				return new List<Document> { new Document(DocumentContent, FileName, DocumentType.LessonVocabulary) };
			}
		}

		protected override Wizard Wizard
		{
			get
			{
				if (_w == null)
				{
					_w = new Wizard(1);
					_w.AttachStepData(Step1, JsonConvert.SerializeObject(StepData));
					_w.GetStepDocuments(Step1).AddOrUpdate(
						new List<WizardStepDocument>
						{
							new WizardStepDocument(FileName, "12", "1111111")
						}
					);
				}

				return _w;
			}
			set
			{
				_w = value;
			}
		}

		[Fact]
		public void WizardStillContainsOneDocument()
		{
			Assert.Single(Wizard.GetStepDocuments(Step1));
		}

		[Fact]
		public void WizardDocumentHasUpdatedProperties()
		{
			var document = Wizard.GetStepDocuments(Step1);
			Assert.Equal(FileName, document.Single().FileName);
			Assert.Equal(HashAlgorithm.ComputeHashString(DocumentContent), document.Single().Hash);
			Assert.Equal("1", document.Single().FileId);
		}
	}

	public class WhenSavingWizardWithExistingDocumentAndNoUpdates : SaveWizardCommandHandlerTests
	{
		private string _fileId = "12";

		private Wizard _w;

		private string Hash = "1111111";

		protected override string WizardId => "5";

		protected override List<Document> Documents
		{
			get
			{
				return new List<Document> { new Document(null, FileName, DocumentType.LessonVocabulary) };
			}
		}

		protected override Wizard Wizard
		{
			get
			{
				if (_w == null)
				{
					_w = new Wizard(1);
					_w.AttachStepData(Step1, JsonConvert.SerializeObject(StepData));
					_w.GetStepDocuments(Step1).AddOrUpdate(
						new List<WizardStepDocument>
						{
						new WizardStepDocument(FileName, _fileId, Hash)
						}
					);
				}

				return _w;
			}
			set
			{
				_w = value;
			}
		}

		[Fact]
		public void WizardStillContainsOneDocument()
		{
			Assert.Single(Wizard.GetStepDocuments(Step1));
		}

		[Fact]
		public void WizardDocumentPropertiesHasNotChanged()
		{
			var document = Wizard.GetStepDocuments(Step1);
			Assert.Equal(FileName, document.Single().FileName);
			Assert.Equal(Hash, document.Single().Hash);
			Assert.Equal(_fileId, document.Single().FileId);
		}
	}
}
