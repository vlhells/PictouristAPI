using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Areas.Admin.Services;
using PictouristAPI.Areas.Admin.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
			if (id != null)
			{
                User user = await _usersService.ChangePasswordAsync(id);
                if (user != null)
                {
                    ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
                    return new ObjectResult(model);
                }
            }
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ChangePasswordAsync(ChangePasswordViewModel model)
		{
			var errors = string.Empty;
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
                        errors += $"{error.Description}<br>";
                    }
				}
			}
			else
			{
				var nf = "Пользователь не найден";
                ModelState.AddModelError(string.Empty, nf);
                errors += $"{nf}<br>";
            }

			return BadRequest(errors);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(CreateUserViewModel model)
		{
			var errors = string.Empty;
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
						errors += $"{error.Description}<br>";
					}
                }
			}
			return BadRequest(errors);
		}

		[HttpGet]
		public async Task<IActionResult> EditAsync(string id)
		{
			if (id != null)
			{
                User user = await _usersService.EditAsync(id);
                if (user != null)
                {
                    EditUserViewModel model = new EditUserViewModel { Email = user.Email, Login = user.UserName, Birthdate = user.Birthdate };
                    return new ObjectResult(model);
                }
            }
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> EditAsync(EditUserViewModel model)
		{
			var errors = string.Empty;
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
                        errors += $"{error.Description}<br>";
                    }
				}
			}
			return BadRequest(errors);
		}

		[HttpPost]
		public async Task<ActionResult> DeleteAsync(string id)
		{
			var result = await _usersService.DeleteAsync(id);
			if (result != null)
			{
				return RedirectToAction("Index");
			}
			return NotFound();
		}
	}
}
