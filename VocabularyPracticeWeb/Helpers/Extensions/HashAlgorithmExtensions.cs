using System;
using System.Security.Cryptography;

namespace VocabularyPracticeWeb.Helpers.Extensions
{
	public static class HashAlgorithmExtensions
	{
		public static string ComputeHashString(this HashAlgorithm algorithm, byte[] array)
		{
			var hash = algorithm.ComputeHash(array);

			return Convert.ToBase64String(hash);
		}
	}
}
