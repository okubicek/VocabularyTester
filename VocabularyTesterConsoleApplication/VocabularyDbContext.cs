using Microsoft.EntityFrameworkCore;

namespace VocabularyPracticeConsoleApplication
{
	public class VocabularyDbContext : DbContext
	{
		private string _connectionstring = "Server=localhost\\SQLEXPRESS;Database=VocabularyTestResults;Trusted_Connection=True;MultipleActiveResultSets=true";

		public DbSet<Answer> Answers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			builder.UseSqlServer(_connectionstring);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder
				.Entity<Answer>()
				.Property(typeof(int), "Id");

			builder
				.Entity<Answer>()
				.HasKey("Id");
		}
	}
}
