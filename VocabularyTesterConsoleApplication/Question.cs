namespace VocabularyPracticeConsoleApplication
{
	public class Question
	{
		private Question()
		{
		}

		public Question(string questionToAsk, string correctAnswer)
		{
			QuestionToAsk = questionToAsk;
			CorrectAnswer = correctAnswer;
		}

		public string QuestionToAsk { get; private set; }

		public string CorrectAnswer { get; private set; }

		public Answer Answer(string answer)
		{
			var isCorrect = answer.Equals(CorrectAnswer);

			return new Answer(QuestionToAsk, CorrectAnswer, answer, isCorrect);
		}
	}
}
