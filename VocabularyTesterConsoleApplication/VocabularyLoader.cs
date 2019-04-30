using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VocabularyPracticeConsoleApplication
{
	public class VocabularyLoader
	{
		public List<Word> LoadVocabulary()
		{
			Console.WriteLine("Enter name of file from which you wish to load questions");
			var fileName = Console.ReadLine();

			return GetVocabulary(fileName);
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
