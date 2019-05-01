using System;
using System.Collections.Generic;
using VocabularyPracticeConsoleApplication;

namespace VocabularyTesterConsoleApplication
{
	public class Program
	{
		private static int _numberOfQuestionsToAsk = 20;

		static void Main(string[] args)
		{
			var vocabulary = LoadVocabulary();
			var testResults = RunTest(vocabulary);

			SaveTestResults(testResults);

			Console.WriteLine($"{testResults.AnsweredCorrectly()} asnwered correctly. " +
				$"You made { testResults.AnsweredIncorrectly() } mistakes. " +
				$"Press any button to end session");

			Console.Read();
		}

		private static TestResults RunTest(List<Word> vocabulary)
		{
			var questionGenerator = new QuestionGenerator(vocabulary);
			var askedQuestions = new TestResults();

			for (int i = 0; i < _numberOfQuestionsToAsk; i++)
			{
				var question = questionGenerator.NextQuestion();

				Console.WriteLine($"Please translate {question.QuestionToAsk} :");
				var answer = Console.ReadLine();

				var answerResult = question.Answer(answer);
				var userMessage = answerResult.IsCorrect ?
						"Correct" :
						$"Wrong! Correct answer is {question.CorrectAnswer}";

				Console.WriteLine(userMessage);

				askedQuestions.AddAnswer(answerResult);
			}

			return askedQuestions;
		}		

		private static List<Word> LoadVocabulary()
		{
			var loader = new VocabularyLoader();
			return loader.LoadVocabulary();
		}

		private static void SaveTestResults(TestResults testResults)
		{
			var repo = new TestResultsRepository();
			repo.Save(testResults);
		}
	}
}
