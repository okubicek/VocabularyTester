using System;

namespace VocabularyPracticeConsoleApplication
{
	public class Answer
	{
		public Answer(string askedQuestion, string correctAnswer, string asnwered, bool isCorrect)
		{
			AskedQuestion = askedQuestion;
			CorrectAnswer = correctAnswer;
			Asnwered = asnwered;
			IsCorrect = isCorrect;
			AnsweredAt = DateTime.Now;
		}

		public string AskedQuestion { get; private set; }

		public string CorrectAnswer { get; private set; }

		public string Asnwered { get; private set; }

		public bool IsCorrect { get; private set; }

		public DateTime AnsweredAt { get; private set; }
	}
}
