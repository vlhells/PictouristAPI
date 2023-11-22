using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Services;

namespace PictouristAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UsersController : Controller
	{
		private IUsersService _usersService;

		public UsersController(IUsersService usersService)
		{
			_usersService = usersService;
		}

		[HttpGet]
		public async Task<IActionResult> IndexAsync()
		{
			var result = await _usersService.IndexAsync();
            if (result != null)
            {
                return new ObjectResult(result);
            }
            return BadRequest("No users exists in db.");
		}

		[HttpGet]
		public async Task<IActionResult> MyPageAsync(string authedUserName)
		{
			var result = await _usersService.MyPageAsync(authedUserName);
			if (result != null)
			{
                return new ObjectResult(result);
            }
			return BadRequest("You are not authentified.");
		}
	}
}
