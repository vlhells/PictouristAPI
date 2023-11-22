using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.ViewModels;
using System;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace PictouristAPI.Services
{
	public class AccountService: IAccountService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<IdentityResult> ChangePasswordAsync(string UserName, string OldPassword, string NewPassword)
		{
			IdentityResult result = null;
            if (UserName != null)
			{
				User user = await _userManager.FindByNameAsync(UserName);
				result =
						await _userManager.ChangePasswordAsync(user, OldPassword, NewPassword);
			}
			return result;
		}

		public async Task<SignInResult> LoginAsync(LoginViewModel model)
		{
			return await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
		}

		public async Task LogoutAsync()
		{
			// Удаление аутен. куки.
			await _signInManager.SignOutAsync();
		}

		public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
		{
			User user = new User(model);
			var result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
			{
				// cookies:
				await _signInManager.SignInAsync(user, false);
			}

			return result;
		}
	}
}
