using System.ComponentModel.DataAnnotations;

namespace VocabularyPracticeWeb.Models.Authentication
{
    public class PasswordResetViewModel
    {
		public string Code { get; set; }

		[EmailAddress]
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Compare("Password")]
		[Required]
		public string ConfirmPassword { get; set; }
	}
}
