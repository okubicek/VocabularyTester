using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Common.Cqrs;

namespace VocabularyPracticeWeb.Domain.Users
{
    public class ApplicationUserCollectionQueryHandler : IQuery<IEnumerable<ApplicationUser>, ApplicationUserCollectionQuery>
	{
		private ApplicationDbContext _dbContext;

		public ApplicationUserCollectionQueryHandler(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<ApplicationUser> Get(ApplicationUserCollectionQuery query)
		{
			var users = _dbContext.Users.AsQueryable();

			if(!string.IsNullOrEmpty(query.FirstName))
			{
				users = users.Where(x => x.FirstName.Contains(query.FirstName));
			}

			if (!string.IsNullOrEmpty(query.Surname))
			{
				users = users.Where(x => x.Surname.Contains(query.Surname));
			}

			if (!string.IsNullOrEmpty(query.Email))
			{
				users = users.Where(x => x.Email.Contains(query.Email));
			}

			if (!string.IsNullOrEmpty(query.Role))
			{
				users = users.Where(x => x.UserRoles.Any(ur => ur.Role.Name == query.Role));
			}

			users = users.Where(x => !x.IsDisabled);

			return users.Include(x => x.UserRoles).ThenInclude(ur => ur.Role);
		}
    }
}
