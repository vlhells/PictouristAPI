using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PictouristAPI.Services;
using PictouristAPI.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PictouristAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
    public class AccountController : ControllerBase
    {
		private IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost]
		public async Task<IActionResult> ChangePasswordAsync(string OldPassword, string NewPassword, string authedUserName)
		{
			var errors = string.Empty;

			var result = await _accountService.ChangePasswordAsync(authedUserName, OldPassword, NewPassword);

			if (result.Succeeded)
			{
				return Ok("Вы успешно сменили пароль.");
			}
			//else
			//{
			//	//ModelState.AddModelError(string.Empty, "Неверный пароль");
			//	//foreach (var error in result.Errors)
			//	//{
			//	//    errors += $"{error.Description}<br>";
			//	//}
			//	errors += "Неверный пароль";
			//}

			return BadRequest("Incorrect input.");
		}

		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			return new ObjectResult(new LoginViewModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> LoginAsync(LoginViewModel model)
		{
			var errors = string.Empty;
			if (ModelState.IsValid)
			{
				var result =
					await _accountService.LoginAsync(model);
				if (result.Succeeded)
				{
					// Принадлежит ли URL приложению.
					if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
					{
						return Redirect(model.ReturnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					string err = "Неверный логин и (или) пароль";
                    ModelState.AddModelError("", err);
                    errors += $"{err}<br>";
                }
			}

			return BadRequest(errors);
		}

		[HttpPost] // TODO: check by swagger.
		//[ValidateAntiForgeryToken]
		//[Authorize]
		public async Task<IActionResult> LogoutAsync()
		{
			await _accountService.LogoutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
		{
			var errors = string.Empty;
			if (ModelState.IsValid)
			{
				var result = await _accountService.RegisterAsync(model);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
                        errors += $"{error.Description}<br>";
                    }
				}
			}
			return BadRequest(errors);
		}
    }
}
