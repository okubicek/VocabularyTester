namespace VocabularyPracticeConsoleApplication
{
	public class TestResultsRepository
	{ 	
		public void Save(TestResults results)
		{
			using (var context = new VocabularyDbContext())
			{
				context.Answers.AddRange(results.Answers);
				context.SaveChanges();
			}
		}
	}
}
