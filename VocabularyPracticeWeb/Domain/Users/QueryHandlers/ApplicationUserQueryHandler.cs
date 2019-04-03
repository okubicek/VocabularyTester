using Common.Cqrs;
using System.Linq;

namespace VocabularyPracticeWeb.Domain.Users
{
	public class ApplicationUserQueryHandler : IQuery<ApplicationUser, ApplicationUserQuery>
	{
		private ApplicationDbContext _dbContext;

		public ApplicationUserQueryHandler(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public ApplicationUser Get(ApplicationUserQuery query)
		{
			var users = _dbContext.Users.AsQueryable();

			if (!string.IsNullOrEmpty(query.UserId))
			{
				users = users.Where(x => x.Id == query.UserId);
			}

			users = users.Where(x => !x.IsDisabled);

			return users.ToList().FirstOrDefault();
		}
	}
}