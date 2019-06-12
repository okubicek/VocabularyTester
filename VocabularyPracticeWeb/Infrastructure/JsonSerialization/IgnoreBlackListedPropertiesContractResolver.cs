using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VocabularyPracticeWeb.Areas.Lessons;

namespace VocabularyPracticeWeb.Infrastructure.JsonSerialization
{
	public class IgnoreBlacklistedPropertiesContractResolver : DefaultContractResolver
	{
		private Dictionary<Type, HashSet<string>> _blacklist = new Dictionary<Type, HashSet<string>>();

		public IgnoreBlacklistedPropertiesContractResolver()
		{
			_blacklist.Add(typeof(LessonViewModel), new HashSet<string> { "File" });
		}

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);

			if (_blacklist.ContainsKey(property.DeclaringType) && 
					_blacklist[property.DeclaringType].Equals(property.PropertyName))
			{
				property.ShouldSerialize = x => { return false; };
			}

			if (!property.Writable)
			{
				var propInfo = member as PropertyInfo;
				if (propInfo != null)
				{
					property.Writable = propInfo.GetSetMethod(true) != null;
				}
			}

			return property;
		}
	}
}
