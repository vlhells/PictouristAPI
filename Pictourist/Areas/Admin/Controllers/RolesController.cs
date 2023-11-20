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
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync() => new ObjectResult(await _rolesService.IndexAsync());

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string name)
        {
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
				}
			}
			return new ObjectResult(name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _rolesService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UserList() => new ObjectResult(await _rolesService.UserList());

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
