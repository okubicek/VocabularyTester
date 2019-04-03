using System.ComponentModel.DataAnnotations;

namespace VocabularyPracticeWeb.Areas.UserManagement
{
    public class RegisterUserViewModel
    {
		public string Email { get; set; }

		public string Password { get; set; }

		[Compare("Password")]
		public string ConfirmPassword { get; set; }

		public string FirstName { get; set; }

		public string Surname { get; set; }

		public bool IsAdmin { get; set; }

		public string ReturnUrl { get; set; }
	}
}
