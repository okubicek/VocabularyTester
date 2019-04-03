using Common.Cqrs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using VocabularyPracticeWeb.Domain.Users;
using VocabularyPracticeWeb.Helpers.Extensions;
using VocabularyPracticeWeb.Models.UserMaganement;

namespace VocabularyPracticeWeb.Areas.UserManagement.Controllers
{
	[Area("UserManagement")]
	[Authorize(Policy = "IsAdministrator")]
	public class UsersController : Controller
    {
		private UserManager<ApplicationUser> _userManager;

		private RoleManager<ApplicationRole> _roleManager;

		private IQuery<IEnumerable<ApplicationUser>, ApplicationUserCollectionQuery> _users;

		public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
			IQuery<IEnumerable<ApplicationUser>, ApplicationUserCollectionQuery> users)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_users = users;
		}

		[HttpGet]
		public IActionResult Index(UsersSearchModel model)
		{
			var users = _users.Get(new ApplicationUserCollectionQuery {
				Email = model.Email,
				FirstName = model.FirstName,
				Surname = model.Surname,
				Role = model.Role
			});

			return View(new UsersViewModel
			{
				Users = users.Select(x => new UserViewModel
				{
					Email = x.Email,
					FirstName = x.FirstName,
					Surname = x.Surname,
					UserId = x.Id,
					IsLocked = x.IsLocked()
				}).ToList(),
				
				Roles = _roleManager.Roles.ToSelectListItems(x => x.Name, x => x.Name, x => x.Name.Equals(model.Role), true),
				Search = model
			});
		}

		[HttpPost]
		public IActionResult Index(IEnumerable<UsersViewModel> model)
		{
			return View(model);
		}
    }
}