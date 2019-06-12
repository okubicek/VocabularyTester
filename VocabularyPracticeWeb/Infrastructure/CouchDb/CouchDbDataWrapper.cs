namespace VocabularyPracticeWeb.Infrastructure.CouchDb
{
	public class CouchDbDataWrapper<T> where T : class
	{
		public string RevId { get; set; }

		public string Id { get; set; }

		public T Data { get; set; }
	}
}