using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VocabularyPracticeConsoleApplication;

namespace VocabularyTesterConsoleApplication
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Enter name of file from which you wish to load questions");
			var fileName = Console.ReadLine();

			var vocabulary = GetVocabulary(fileName);

			int correctAnswers = RunTest(vocabulary);

			Console.WriteLine($"{correctAnswers} asnwered correctly. You made {20 - correctAnswers} mistakes. Press any button to end session");
			Console.Read();
		}

		private static int RunTest(List<Word> vocabulary)
		{
			var rnd = new Random((int)DateTime.Now.Ticks);
			var correctAnswers = 0;

			for (int i = 0; i <= 20; i++)
			{
				var index = rnd.Next(vocabulary.Count() - 1);

				Console.WriteLine($"Please translate {vocabulary[index].Translation} :");
				var answer = Console.ReadLine();
				if (answer.Equals(vocabulary[index].ForeignWord))
				{
					Console.WriteLine("Correct");
					correctAnswers++;
				}
				else
				{
					Console.WriteLine($"Wrong! Correct answer is {vocabulary[index].ForeignWord}");
				}
			}

			return correctAnswers;
		}

		private static List<Word> GetVocabulary(string fileName)
		{
			var lines = File.ReadAllLines($"..\\..\\..\\..\\..\\Vocabulary\\{fileName}.txt");
			return lines
				.Select(x => CreateWord(x))
				.ToList();
		}

		private static Word CreateWord(string line)
		{
			var components = line.Split("|");

			return new Word(components[1], components[0], components[2]);
		}
	}
}
