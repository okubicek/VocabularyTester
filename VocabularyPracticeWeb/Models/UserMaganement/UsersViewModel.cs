using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VocabularyPracticeWeb.Models.UserMaganement
{
    public class UsersViewModel
    {
		public List<UserViewModel> Users { get; set; }

		public UsersSearchModel Search { get; set; }

		public IEnumerable<SelectListItem> Roles { get; set; }
	}
}
