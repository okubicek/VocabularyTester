using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace VocabularyPracticeWeb.Helpers.Validators
{
	public class FileTypeAttribute : ValidationAttribute
	{
		private string _contentType;

		public FileTypeAttribute(string contentType)
		{
			_contentType = contentType;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			if (file != null && !file.ContentType.Equals(_contentType))
			{
				return new ValidationResult("Only text file can be uploaded");
			}

			return ValidationResult.Success;
		}
	}
}
