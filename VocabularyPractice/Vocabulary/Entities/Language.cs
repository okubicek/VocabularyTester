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

		public static bool operator ==(Language first, Language second)
		{
			if (first.Id.HasValue && second.Id.HasValue)
			{
				return first.Id.Value == second.Id.Value;
			}

			return first.Name == second.Name;
		}

		public static bool operator !=(Language first, Language second)
		{
			return !(first == second);
		}
	}
}
