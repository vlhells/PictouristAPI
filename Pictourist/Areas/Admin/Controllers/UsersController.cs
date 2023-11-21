using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Areas.Admin.Services;
using PictouristAPI.Areas.Admin.ViewModels;

namespace PictouristAPI.Areas.Admin.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Area("Admin")]
	//[Authorize(Roles = "Admin")]
	public class UsersController : ControllerBase
	{
		private IUsersService _usersService;

		public UsersController(IUsersService usersService)
		{
			_usersService = usersService;
		}

		[HttpGet]
		public async Task<IActionResult> ChangePasswordAsync(string id)
		{
			User user = await _usersService.ChangePasswordAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
			return new ObjectResult(model);
		}

		[HttpPost]
		public async Task<IActionResult> ChangePasswordAsync(ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var _passwordValidator =
					HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
				var _passwordHasher =
					HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

				IdentityResult result = await _usersService.ChangePasswordAsync(model, _passwordValidator, _passwordHasher);

				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Пользователь не найден");
			}

			return new ObjectResult(model);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(CreateUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _usersService.CreateAsync(model);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
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

		[HttpGet]
		public async Task<IActionResult> EditAsync(string id)
		{
			User user = await _usersService.EditAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			EditUserViewModel model = new EditUserViewModel { Email = user.Email, Login = user.UserName, Birthdate = user.Birthdate };
			return new ObjectResult(model);
		}

		[HttpPost]
		public async Task<IActionResult> EditAsync(EditUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _usersService.EditAsync(model);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
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

		[HttpPost]
		public async Task<ActionResult> DeleteAsync(string id)
		{
			await _usersService.DeleteAsync(id);
			return RedirectToAction("Index");
		}
	}
}
