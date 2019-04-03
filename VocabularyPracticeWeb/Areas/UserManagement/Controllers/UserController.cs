using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VocabularyPracticeWeb.Domain.Users;
using VocabularyPracticeWeb.Helpers.Extensions;
using VocabularyPracticeWeb.Models.UserMaganement;
using VocabularyPracticeWeb.Controllers.Authentication;
using VocabularyPracticeWeb.Models.Authentication;
using System.Collections.Generic;
using Common.Cqrs;

namespace VocabularyPracticeWeb.Areas.UserManagement.Controllers
{
	[Area("UserManagement")]
	[Authorize(Policy = "IsAdministrator")]
	public class UserController : Controller
    {
		private IQuery<ApplicationUser, ApplicationUserQuery> _user;

		private UserManager<ApplicationUser> _userManager;

		private IAuthorizationService _authorizationService;

		public UserController(IQuery<ApplicationUser, ApplicationUserQuery> user, 
			UserManager<ApplicationUser> userManager, 
			IAuthorizationService authorizationService)
		{
			_user = user;
			_userManager = userManager;
			_authorizationService = authorizationService;
		}

		[HttpGet]
        public IActionResult Edit(string userId)
        {
			var user = GetUserModel<EditUserViewModel>(userId);

			return ViewComponent("EditUser", new { model = user });
        }

		[HttpPost]
		public async Task<IActionResult> Edit(EditUserViewModel model)
		{
			var dbUser = GetApplicationUserFromDb(model.UserId);

			if (dbUser.IsLocked() != model.IsLocked)
			{
				if (dbUser.IsLocked()) dbUser.Unlock(); else dbUser.Lock();
			}

			dbUser.FirstName = model.FirstName;
			dbUser.Surname = model.Surname;

			var res = await _userManager.UpdateAsync(dbUser);
			if (!res.Succeeded)
			{
				ModelState.AddModelIdentityErrors(res.Errors);
				return View(model);
			}
			
			ViewData["info-message"] = "User has been successfully edited";
			model.IsPosted = true;

			//return View(model);
			return ViewComponent("EditUser", new { model = model });
		}

		[HttpGet]
		public IActionResult Remove(string userId)
		{
			var user = GetUserModel<RemoveUserViewModel>(userId);

			return ViewComponent("RemoveUser", new { model = user });
		}

		[HttpPost]
		public async Task<IActionResult> Remove(RemoveUserViewModel model)
		{
			ApplicationUser user = GetApplicationUserFromDb(model.UserId);

			user.PasswordHash = null;
			user.IsDisabled = true;

			var res = await _userManager.UpdateAsync(user);
			if (!res.Succeeded)
			{
				ModelState.AddModelIdentityErrors(res.Errors);
				return View(model);
			}

			ViewData["info-message"] = "User has been successfully Removed";
			model.IsPosted = true;

			return ViewComponent("Remove", new { model = user });
		}

		private ApplicationUser GetApplicationUserFromDb(string userId)
		{
			return _user.Get(new ApplicationUserQuery
			{
				UserId = userId
			});
		}

		public T GetUserModel<T>(string userId) where T : UserViewModel, new()
		{
			var user = GetApplicationUserFromDb(userId);

			return new T
			{
				UserId = user.Id,
				FirstName = user.FirstName,
				Surname = user.Surname,
				Email = user.Email,
				IsLocked = user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.Now
			};
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register(string ReturnUrl)
		{
			return View(new RegisterUserViewModel
			{
				ReturnUrl = ReturnUrl
			});
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(RegisterUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await CreateNewUser(model);

					return LocalRedirect("/authentication/login");
				}
				catch (IdentityException ex)
				{
					ModelState.AddModelIdentityErrors(ex.ErrorCodes);
				}
			}

			return View(model);
		}

		private async Task CreateNewUser(RegisterUserViewModel model)
		{
			var user = new ApplicationUser
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				Surname = model.Surname,
				SecurityStamp = string.Empty
			};

			var role = model.IsAdmin ? AuthorisationRoles.Admin : AuthorisationRoles.User;

			if (model.IsAdmin && !this.User.IsInRole("admin"))
			{
				throw new ApplicationException("Unauthorised Access");
			}

			await ExecuteIdentityFunction(() => _userManager.CreateAsync(user, model.Password));
			await ExecuteIdentityFunction(() => _userManager.AddToRoleAsync(user, role));
		}

		private async Task ExecuteIdentityFunction(Func<Task<IdentityResult>> func)
		{
			var res = await func();
			if (!res.Succeeded)
			{
				throw new IdentityException(res.Errors);
			}
		}
	}
}