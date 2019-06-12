using Microsoft.AspNetCore.Http;
using System.IO;

namespace VocabularyPracticeWeb.Helpers.Extensions
{
	public static class IFormFileExtensions
	{
		public static byte[] ToByteArray(this IFormFile formFile)
		{
			using (var memoryStream = new MemoryStream())
			{
				formFile.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
		}
	}
}
