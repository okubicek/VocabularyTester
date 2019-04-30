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
			int correctAnswers = RunTest(vocabulary);

			Console.WriteLine($"{correctAnswers} asnwered correctly. You made {_numberOfQuestionsToAsk - correctAnswers} mistakes. Press any button to end session");
			Console.Read();
		}

		private static int RunTest(List<Word> vocabulary)
		{
			var questionGenerator = new QuestionGenerator(vocabulary);
			var correctAnswers = 0;

			for (int i = 0; i <= _numberOfQuestionsToAsk; i++)
			{
				var wordToAsk = questionGenerator.NextQuestion();

				Console.WriteLine($"Please translate {wordToAsk.Translation} :");
				var answer = Console.ReadLine();
				if (answer.Equals(wordToAsk.ForeignWord))
				{
					Console.WriteLine("Correct");
					correctAnswers++;
				}
				else
				{
					Console.WriteLine($"Wrong! Correct answer is {wordToAsk.ForeignWord}");
				}
			}

			return correctAnswers;
		}		

		private static List<Word> LoadVocabulary()
		{
			var loader = new VocabularyLoader();
			return loader.LoadVocabulary();
		}
	}
}
