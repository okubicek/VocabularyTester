using System.Collections.Generic;
using VocabularyPracticeDomain.Vocabulary;

namespace VocabularyPracticeDomain.Lessons
{
	public class LessonVocabulary : List<Word>
	{
		public LessonVocabulary(IEnumerable<Word> vocabulary)
		{
			this.AddRange(vocabulary);
		}
	}
}
