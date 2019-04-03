using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace VocabularyPracticeWeb.Domain.Users
{
    public class ApplicationRole : IdentityRole
    {
		public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
