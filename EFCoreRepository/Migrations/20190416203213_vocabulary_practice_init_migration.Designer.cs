﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VocabularyPracticeEFCoreRepository;

namespace VocabularyPracticeEFCoreRepository.Migrations
{
    [DbContext(typeof(VocabularyPracticeDbContext))]
    [Migration("20190416203213_vocabulary_practice_init_migration")]
    partial class vocabulary_practice_init_migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VocabularyPracticeDomain.Lessons.Lesson", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("LessonId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("ForeignLanguageId")
                        .IsRequired();

                    b.Property<int?>("NativeLanguageId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ForeignLanguageId");

                    b.HasIndex("NativeLanguageId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("VocabularyPracticeDomain.Lessons.LessonVocabulary", b =>
                {
                    b.Property<int>("LessonId");

                    b.Property<int>("NativeWordId");

                    b.Property<int?>("ForeignWordId");

                    b.Property<int?>("LessonId1");

                    b.HasKey("LessonId", "NativeWordId", "ForeignWordId");

                    b.HasIndex("ForeignWordId");

                    b.HasIndex("LessonId1");

                    b.HasIndex("NativeWordId");

                    b.ToTable("LessonVocabulary");
                });

            modelBuilder.Entity("VocabularyPracticeDomain.Vocabulary.Language", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("LanguageId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("VocabularyPracticeDomain.Vocabulary.Word", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("WordId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("LanguageId")
                        .IsRequired();

                    b.Property<string>("Pronoun");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("VocabularyPracticeDomain.Lessons.Lesson", b =>
                {
                    b.HasOne("VocabularyPracticeDomain.Vocabulary.Language", "ForeignLanguage")
                        .WithMany()
                        .HasForeignKey("ForeignLanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VocabularyPracticeDomain.Vocabulary.Language", "NativeLanguage")
                        .WithMany()
                        .HasForeignKey("NativeLanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VocabularyPracticeDomain.Lessons.LessonVocabulary", b =>
                {
                    b.HasOne("VocabularyPracticeDomain.Vocabulary.Word", "ForeignWord")
                        .WithMany()
                        .HasForeignKey("ForeignWordId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VocabularyPracticeDomain.Lessons.Lesson", "Lesson")
                        .WithMany()
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VocabularyPracticeDomain.Lessons.Lesson")
                        .WithMany("LessonVocabulary")
                        .HasForeignKey("LessonId1");

                    b.HasOne("VocabularyPracticeDomain.Vocabulary.Word", "NativeWord")
                        .WithMany()
                        .HasForeignKey("NativeWordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VocabularyPracticeDomain.Vocabulary.Word", b =>
                {
                    b.HasOne("VocabularyPracticeDomain.Vocabulary.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
