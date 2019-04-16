namespace VocabularyPracticeDomain.Vocabulary
{
    public class Language : EntityBase
    {
		private Language()
		{
		}

		public Language(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
    }
}
