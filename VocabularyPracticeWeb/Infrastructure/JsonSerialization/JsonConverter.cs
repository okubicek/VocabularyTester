using Newtonsoft.Json;

namespace VocabularyPracticeWeb.Infrastructure.JsonSerialization
{
	public static class JsonConverter
	{
		public static string ConvertToJson<T>(T data)
		{
			return JsonConvert.SerializeObject(data, GetJsonSettings());
		}

		public static T ConvertToObject<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json, GetJsonSettings());
		}

		private static JsonSerializerSettings GetJsonSettings()
		{
			var settings = new JsonSerializerSettings();
			settings.ContractResolver = new IgnoreBlacklistedPropertiesContractResolver();
			return settings;
		}
	}
}
