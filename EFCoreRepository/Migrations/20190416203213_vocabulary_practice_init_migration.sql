IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;


create table [dbo].[Languages](
	[LanguageId] [smallint] identity(1,1) NOT NULL,
	[Name] [varchar](64) NOT NULL,
	constraint [PK_LanguageId] primary key clustered(
		[LanguageId]
	)
)

GO
create table [dbo].[Lessons](
	[LessonId] [int] identity(1,1) NOT NULL,
	[Description] [varchar](128) NOT NULL,
	[NativeLanguageId] [smallint] NOT NULL,
	[ForeignLanguageId] [smallint] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	constraint [PK_Lessons] primary key clustered (
		[LessonId]
	)
)
GO

ALTER TABLE [dbo].[Lessons] ADD  CONSTRAINT [Def_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

alter table [dbo].[Lessons]  with check add  constraint [FK_ForeignLanguageId] foreign key([ForeignLanguageId])
references [dbo].[Languages] ([LanguageId])
GO

alter table [dbo].[Lessons]  with check add  constraint [FK_NativeLanguageId] foreign key([NativeLanguageId])
references [dbo].[Languages] ([LanguageId])
GO

create table [dbo].[Words](
	[WordId] [int] identity(1,1) NOT NULL,
	[Word] [nvarchar](128) NOT NULL,
	[Pronoun] [nvarchar](10) NULL,
	[LanguageId] [smallint] NOT NULL,
	DateCreated dateTime not null
	constraint [PK_Vocabulary] primary key clustered (
		WordId
	)
)
GO
ALTER TABLE [dbo].[Words] ADD  CONSTRAINT [Def_Words_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

alter table [dbo].[Words]  with check add  constraint [FK_WordLanguage] foreign key([LanguageId])
references [dbo].[Languages] ([LanguageId])
GO

CREATE TABLE [dbo].[LessonVocabulary](
	[NativeWordId] [int] NOT NULL,
	[ForeignWordId] [int] NOT NULL,
	[LessonId] [int] NOT NULL,
	DateCreated datetime not null,
	constraint [PK__LessonVocabulary] primary key clustered (
		[NativeWordId],
		[ForeignWordId],
		[LessonId]
	)
) 
GO
alter table [dbo].[LessonVocabulary] add  constraint [Def_LessonVocabulary_DateCreated]  default (getdate()) for [DateCreated]
GO
alter table [dbo].[LessonVocabulary]  with check add  constraint [FK__ForeignWordId] foreign keY([ForeignWordId])
references [dbo].[Words] ([WordId])
go

alter table [dbo].[LessonVocabulary]  with check add  constraint [FK__LessonId] foreign key([LessonId])
references [dbo].[Lessons] ([LessonId])
go

alter table [dbo].[LessonVocabulary]  with check add  constraint [FK__NativeWordId] foreign key([NativeWordId])
references [dbo].[Words] ([WordId])
go

