using Common.Cqrs;
using System.Linq;
using VocabularyPracticeApplication.Api;
using VocabularyPracticeDomain.Lessons;
using VocabularyPracticeDomain.Vocabulary;

namespace VocabularyPracticeApplication.Lessons
{
	public class CreateNewLessonCommandHandler : ICommand<CreateNewLessonCommand>
	{
		ILessonRepository _lessonRepo;

		ILanguageRepository _languageRepo;

		public CreateNewLessonCommandHandler(ILessonRepository lessonRepo, 
			ILanguageRepository languageRepo
)
		{
			_lessonRepo = lessonRepo;
			_languageRepo = languageRepo;
		}

		public void Execute(CreateNewLessonCommand command)
		{
			var languages = GetLanguagePair(command);

			var lesson = new Lesson(command.Description, languages.Native, languages.Foreign);
			foreach(var words in command.Vocabulary)
			{
				var nativeWord = new VocabularyPracticeDomain.Vocabulary.Word(languages.Native, words.Native.Pronoun, words.Native.Text);
				var foreignWord = new VocabularyPracticeDomain.Vocabulary.Word(languages.Foreign, words.Learned.Pronoun, words.Learned.Text);

				lesson.AddVocabulary(nativeWord, foreignWord);
			}

			_lessonRepo.Add(lesson);
		}

		private (Language Native, Language Foreign) GetLanguagePair(CreateNewLessonCommand command)
		{
			var languages = _languageRepo.GetLanguageCollection();

			return (
				Native: languages.First(x => x.Id == command.NativeLanguageId),
				Foreign: languages.First(x => x.Id == command.NativeLanguageId)
			);
		}
	}
}
