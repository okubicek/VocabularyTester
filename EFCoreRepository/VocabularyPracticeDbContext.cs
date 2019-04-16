using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using VocabularyPracticeDomain.Lessons;
using VocabularyPracticeDomain.Vocabulary;

namespace VocabularyPracticeEFCoreRepository
{
    public class VocabularyPracticeDbContext : DbContext
    {
		public VocabularyPracticeDbContext(DbContextOptions<VocabularyPracticeDbContext> options) : base(options)
		{
		}

		public DbSet<Lesson> Lessons { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder
				.Entity<Language>()
				.ToTable("Languages")
				.HasKey(x => x.Id);
			builder
				.Entity<Language>()
				.Property(x => x.Id).HasColumnName("LanguageId");

			builder
				.Entity<Word>()
				.ToTable("Words")
				.HasKey(x => x.Id);
			builder
				.Entity<Word>()
				.Property(x => x.Id).HasColumnName("WordId");
			builder
				.Entity<Word>()
				.HasOne(x => x.Language)
				.WithMany()
				.IsRequired();

			builder
				.Entity<Lesson>()
				.HasKey(x => x.Id);
			builder
				.Entity<Lesson>()
				.Property(x => x.Id).HasColumnName("LessonId");
			builder.Entity<Lesson>()
				.HasOne(x => x.NativeLanguage)
				.WithMany()
				.IsRequired();
			builder.Entity<Lesson>()
				.HasOne(x => x.ForeignLanguage)
				.WithMany()
				.IsRequired();

			builder.Entity<LessonVocabulary>()
				.HasOne(x => x.Lesson)
				.WithMany()
				.IsRequired().
				HasForeignKey("LessonId");
			builder.Entity<LessonVocabulary>()
				.HasOne(x => x.NativeWord)
				.WithMany()
				.IsRequired().
				HasForeignKey("NativeLanguageId");
			builder.Entity<LessonVocabulary>()
				.HasOne(x => x.ForeignWord)
				.WithMany()
				.IsRequired().
				HasForeignKey("ForeignLanguageId");
			builder.Entity<LessonVocabulary>()
				.HasKey("LessonId", "NativeLanguageId", "ForeignLanguageId");
		}
    }
}
