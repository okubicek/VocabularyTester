using FluentEmail.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VocabularyPracticeWeb.Domain.Users;
using VocabularyPracticeWeb.Helpers.Extensions;
using VocabularyPracticeWeb.Models.Authentication;

namespace VocabularyPracticeWeb.Controllers.Authentication
{
	public class AuthenticationController : Controller
	{
		private UserManager<ApplicationUser> _userManager;

		private RoleManager<ApplicationRole> _roleManager;

		private SignInManager<ApplicationUser> _signInManager;
		
		private IFluentEmail _emailSender;

		public AuthenticationController(UserManager<ApplicationUser> userManager, 
				SignInManager<ApplicationUser> signInManager, 
				RoleManager<ApplicationRole> roleManager,
				IFluentEmail emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_emailSender = emailSender;
		}

		[HttpGet]
		public async Task<IActionResult> Login(string returnUrl)
		{
			return View(new LoginViewModel
			{
				ReturnUrl = returnUrl
			});
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model, string button)
		{
			if (ModelState.IsValid && button.Equals("login"))
			{
				var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);
				if (signInResult.Succeeded)
				{
					var url = string.IsNullOrEmpty(model.ReturnUrl) ? Url.Action("Index", "Home") : model.ReturnUrl;
					return LocalRedirect(url);
				}
			}

			return View(new LoginViewModel
			{
				UserName = model.UserName,
				ReturnUrl = model.ReturnUrl
			});
		}

		[HttpPost]
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();

			return RedirectToAction("LogIn", "Authentication");
		}

		[HttpGet]
		public async Task<IActionResult> ForgottenPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgottenPassword(ForgottenPasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await _userManager.FindByEmailAsync(model.Email);
			if(user != null)
			{
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);

				var url = this.Url.Action("PasswordReset", "Authentication", new { UserId = user.Id, Code = token }, Request.Scheme);

				/*await _emailSender
					.To(user.Email)
					.Subject("Your password has been reset")
					.Body($"Please <a href=\"{url}\">follow linkfollow link</a>")
					.SendAsync();*/
			}

			return RedirectToAction("PasswordSend");
		}

		[HttpGet]
		public async Task<IActionResult> PasswordSend()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> PasswordReset(string code)
		{
			return View(
				new PasswordResetViewModel
					{
						Code = code
					}
				);
		}

		[HttpPost]
		public async Task<IActionResult> PasswordReset(PasswordResetViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user != null)
			{
				var res = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
				if(res.Succeeded)
				{
					return RedirectToAction("ConfirmPasswordReset");
				}
				
				ModelState.AddModelIdentityErrors(res.Errors);
				return View(model);				
			}

			return RedirectToAction("ConfirmPasswordReset");
		}

		[HttpGet]
		public async Task<IActionResult> ConfirmPasswordReset()
		{
			return View();
		}		
	}
}
