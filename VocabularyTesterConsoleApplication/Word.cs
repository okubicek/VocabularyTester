namespace VocabularyPracticeConsoleApplication
{
	public class Word
	{
		public Word(string foreignWord, string pronoun, string translation)
		{
			ForeignWord = foreignWord;
			Pronoun = pronoun;
			Translation = translation;
		}

		public string ForeignWord { get; private set; }

		public string Pronoun { get; private set; }

		public string Translation { get; private set; }
	}
}
