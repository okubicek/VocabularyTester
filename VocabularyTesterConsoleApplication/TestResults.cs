using System.Collections.Generic;
using System.Linq;

namespace VocabularyPracticeConsoleApplication
{
	public class TestResults
	{
		public TestResults()
		{
			_answers = new List<Answer>();
		}

		private List<Answer> _answers;

		public IReadOnlyCollection<Answer> Answers { get { return _answers.AsReadOnly(); } }

		public void AddAnswer(Answer answer)
		{
			_answers.Add(answer);
		}

		public int NumberOfQuestions()
		{
			return _answers.Count;
		}

		public int AnsweredCorrectly()
		{
			return _answers
				.Where(x => x.IsCorrect)
				.Count();
		}

		public int AnsweredIncorrectly()
		{
			return _answers
				.Where(x => !x.IsCorrect)
				.Count();
		}
	}
}
