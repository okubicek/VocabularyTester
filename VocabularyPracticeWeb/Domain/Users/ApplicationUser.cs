using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace VocabularyPracticeWeb.Domain.Users
{
    public class ApplicationUser : IdentityUser
    {
		public string FirstName { get; set; }

		public string Surname { get; set; }

		public bool IsDisabled { get; set; }

		public ICollection<ApplicationUserRole> UserRoles { get; set; }

		public void Lock()
		{
			this.LockoutEnd = DateTime.MaxValue;
		}

		public void Unlock()
		{
			this.LockoutEnd = DateTime.Now;
		}

		public bool IsLocked()
		{
			return this.LockoutEnd.HasValue && this.LockoutEnd.Value > DateTime.Now;
		}
	}
}
