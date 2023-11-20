using Microsoft.AspNetCore.Mvc;
using PictouristAPI.Services;
using PictouristAPI.ViewModels;

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

		// TODO: check by swagger. Fix user.identity.
		[HttpPost]
		public async Task<IActionResult> ChangePasswordAsync(string OldPassword, string NewPassword)
		{
			var errors = string.Empty;

			var result = await _accountService.ChangePasswordAsync(User.Identity.Name, OldPassword, NewPassword);

			if (result.Succeeded)
			{
				return Ok("Вы успешно сменили пароль.");
			}
			else
			{
				//ModelState.AddModelError(string.Empty, "Неверный пароль");
				//foreach (var error in result.Errors)
				//{
				//    errors += $"{error.Description}<br>";
				//}
				errors += "Неверный пароль";
			}

			return Content(errors);
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
					ModelState.AddModelError("", "Неверный логин и (или) пароль");
				}
			}

			return new ObjectResult(model);
		}

		[HttpPost] // TODO: check by swagger.
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogoutAsync()
		{
			await _accountService.LogoutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
		{
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
					}
				}
			}
			return new ObjectResult(model);
		}
    }
}
