﻿using Microsoft.AspNetCore.Identity;

namespace VocabularyPracticeWeb.Domain.Users
{
	public class ApplicationUserRole : IdentityUserRole<string>
	{
		public virtual ApplicationUser User { get; set; }

		public virtual ApplicationRole Role { get; set; }
	}
}
