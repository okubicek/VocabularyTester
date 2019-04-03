using System.ComponentModel.DataAnnotations;

namespace VocabularyPracticeWeb.Models.Authentication
{
    public class ForgottenPasswordViewModel
    {
		[EmailAddress]
		[Required]
		public string Email { get; set; }
    }
}
