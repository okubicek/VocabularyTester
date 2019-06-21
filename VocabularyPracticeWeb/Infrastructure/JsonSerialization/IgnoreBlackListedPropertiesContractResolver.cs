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
			_blacklist.Add(typeof(LessonViewModel), new HashSet<string> { nameof(LessonViewModel.File) });
		}

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);

			if (IsBlackListed(property))
			{
				//property.ShouldSerialize = x => { return false; };
				property.Ignored = true;
			}

			//allow deserialization for private setters
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

		private bool IsBlackListed(JsonProperty property)
		{
			return _blacklist.ContainsKey(property.DeclaringType) &&
								_blacklist[property.DeclaringType].Contains(property.PropertyName);
		}

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			return base.CreateProperties(type, memberSerialization);
		}
	}
}
