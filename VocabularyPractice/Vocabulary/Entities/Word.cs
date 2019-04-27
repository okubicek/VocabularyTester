namespace VocabularyPracticeDomain.Vocabulary
{
    public class Word : EntityBase
    {
		private Word()
		{
		}

		public Word(Language language, string pronoun, string text)
		{
			Language = language;
			Pronoun = pronoun;
			Text = text;
		}

		public Language Language { get; set; }

		public string Pronoun { get; set; }

		public string Text { get; set; }
	}
}
