namespace VocabularyPracticeWeb.Models.UserMaganement
{
    public class UserViewModel
    {
		public string UserId { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string Surname { get; set; }

		public bool IsLocked { get; set; }
	}
}
