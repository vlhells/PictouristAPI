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
			return new ObjectResult(await _usersService.IndexAsync());
		}

		[HttpGet]
		public async Task<IActionResult> MyPageAsync()
		{
			return new ObjectResult(await _usersService.MyPageAsync(User.Identity.Name));
		}
	}
}
