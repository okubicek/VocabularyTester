﻿using System;
using System.Collections.Generic;

namespace VocabularyPracticeConsoleApplication
{
	public class QuestionGenerator
	{
		private List<Word> _vocabulary;

		private HashSet<int> _alreadyAsked;

		private Random _randomNumberGenerator;

		public QuestionGenerator(List<Word> vocabulary)
		{
			_vocabulary = vocabulary;
			_alreadyAsked = new HashSet<int>();
			_randomNumberGenerator = new Random((int)DateTime.Now.Ticks);
		}

		public Question NextQuestion()
		{
			var index = _randomNumberGenerator.Next(_vocabulary.Count - 1);

			if (_alreadyAsked.Contains(index))
			{
				return this.NextQuestion();
			}
		
			_alreadyAsked.Add(index);

			return new Question(_vocabulary[index].Translation, _vocabulary[index].ForeignWord);			
		}
	}
}
