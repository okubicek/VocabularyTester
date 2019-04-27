using Common.Cqrs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VocabularyPracticeWeb.Areas.UserManagement;
using VocabularyPracticeWeb.Areas.UserManagement.Controllers;
using VocabularyPracticeWeb.Domain.Users;
using Xunit;

namespace VocabularyPracticeWeb.Tests.Controllers
{
	public abstract class UserControllerTest
	{
		protected UserController _underTest;

		public UserControllerTest()
		{
			var userQuery = GetUserQuery();
			var userManager = MockUserManager();

			_underTest = new UserController(userQuery, userManager);
		}

		private static IQuery<ApplicationUser, ApplicationUserQuery> GetUserQuery()
		{
			var userQuery = new Mock<IQuery<ApplicationUser, ApplicationUserQuery>>();
			var applicationUser = new ApplicationUser();

			userQuery.Setup(x => x.Get(It.IsAny<ApplicationUserQuery>()))
				.Returns(applicationUser);

			return userQuery.Object;
		}

		protected abstract UserManager<ApplicationUser> MockUserManager();
	}

	public abstract class UserControllerRegisterUserTest : UserControllerTest
	{
		protected override UserManager<ApplicationUser> MockUserManager()
		{
			var userStore = new Mock<IUserStore<ApplicationUser>>().Object;

			var userManager = new Mock<UserManager<ApplicationUser>>(
						userStore, 
						null,
						null,
						new List<IUserValidator<ApplicationUser>>(),
						new List<IPasswordValidator<ApplicationUser>>(),
						null,
						null,
						null,
						null);

			var result = GetIdentityResult();
			
			userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).Returns(Task.FromResult(result));
			userManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(result);

			return userManager.Object;
		}

		protected abstract IdentityResult GetIdentityResult();

		protected static RegisterUserViewModel GetRegisterViewModel(bool isAdmin)
		{
			return new RegisterUserViewModel
			{
				ConfirmPassword = "BleBlaBlu",
				Password = "BleBlaBlue",
				Email = "Mail@something.com",
				FirstName = "Grrrr",
				Surname = "Crrrrr",
				IsAdmin = isAdmin,
				ReturnUrl = ""
			};
		}

		protected void SetupUserRole(string role)
		{
			var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
					{
						new Claim(ClaimTypes.Role, role)
					}));

			_underTest.ControllerContext = new ControllerContext()
			{
				HttpContext = new DefaultHttpContext() { User = user }
			};
		}
	}

	public class UserControllerRegisterUserWithoutIdentityErrorsTests : UserControllerRegisterUserTest
	{
		protected override IdentityResult GetIdentityResult()
		{
			return IdentityResult.Success;
		}

		[Fact]
		public async void CanRegisterNewUserWithoutError()
		{
			await _underTest.Register(GetRegisterViewModel(false));

			Assert.Equal(0, _underTest.ModelState.ErrorCount);
		}

		[Fact]
		public async void NonAdminUserCantCreateAdmin()
		{
			SetupUserRole("user");
			var model = GetRegisterViewModel(true);

			await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _underTest.Register(model));
		}

		[Fact]
		public async void AdminUserCanCreateAdmin()
		{
			SetupUserRole("admin");
			var model = GetRegisterViewModel(true);
			await _underTest.Register(model);

			Assert.Equal(0, _underTest.ModelState.ErrorCount);
		}
	}

	public class UserControllerRegisterUserWithIdentityErrorsTests : UserControllerRegisterUserTest
	{
		protected override IdentityResult GetIdentityResult()
		{
			return IdentityResult.Failed(new IdentityError() { Code = "c", Description = "d" });
		}

		[Fact]
		public async void ModelStateContainsErrorWhenErrorReturnedByUserManager()
		{
			var model = GetRegisterViewModel(false);
			await _underTest.Register(model);

			Assert.Equal(1, _underTest.ModelState.ErrorCount);
		}

	}
}
