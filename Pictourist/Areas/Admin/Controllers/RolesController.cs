using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Areas.Admin.Services;

namespace PictouristAPI.Admin.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var result = await _rolesService.IndexAsync();
            if (result != null)
            {
                return new ObjectResult(result);
            }
            return NotFound("No roles exists in db.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string name)
        {
            var errors = string.Empty;
			IdentityResult result = await _rolesService.CreateAsync(name);
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
			return BadRequest(errors);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _rolesService.DeleteAsync(id);
            if (result != null)
            {
                return RedirectToAction("Index");
            }
            return NotFound("Incorrect role id");
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var result = await _rolesService.UserList();
            if (result != null)
                return new ObjectResult(await _rolesService.UserList());
            return NotFound("No users exists in db");
        }

		[HttpGet]
		public async Task<IActionResult> EditAsync(string userId) // ...
        {
            var model = await _rolesService.EditAsync(userId);
            if (model != null)
            {
                return new ObjectResult(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(string userId, List<string> roles)
        {
            User user = await _rolesService.EditAsync(userId, roles);
            if (user != null)
            {
                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}
