using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace PictouristAPI.Services
{
	public interface IAccountService
	{
		public Task<IdentityResult> ChangePasswordAsync(string UserName, string OldPassword, string NewPassword);
		public Task<SignInResult> LoginAsync(LoginViewModel model);
		public Task LogoutAsync();
		public Task<IdentityResult> RegisterAsync(RegisterViewModel model);
	}
}
