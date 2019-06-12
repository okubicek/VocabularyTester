using ArmChair;
using System;
using System.Collections.Generic;

namespace VocabularyPracticeWeb.Infrastructure.CouchDb
{
	public class CouchDbRepository<T> : IBlobRepository<T> where T : class
	{
		private ISession _db;

		public CouchDbRepository(ISession db)
		{
			_db = db;
		}

		public T GetById(string id)
		{
			return _db.GetById<T>(id);
		}

		public void Save(T data)
		{
			_db.Add(data);
			_db.Commit();
		}

		public void SaveCollection(IEnumerable<T> data)
		{
			_db.AddRange(data);
			_db.Commit();
		}
	}
}
