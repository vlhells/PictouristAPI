using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Services;

namespace PictouristAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize]
	public class FriendsController : ControllerBase
	{
		private IFriendsService _friendsService;

		public FriendsController(IFriendsService friendsService)
		{
			_friendsService = friendsService;
		}

		[HttpGet]
		public async Task<IActionResult> IndexAsync(string Id) // "Friends/Index/Friend1", etc...
		{
			if (Id == null)
			{
				return new ObjectResult(await _friendsService.IndexAsync());
			}
			else
			{
				User u = await _friendsService.IndexAsync(User.Identity.Name, Id);
				if (u != null)
				{
					return new ObjectResult(u);
				}
			}

			return NoContent();
		}

		[HttpPost]
		public async Task<IActionResult> AddFriendAsync(Guid Id)
		{
			return Ok(await _friendsService.AddFriendAsync(User.Identity.Name, Id));
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFriendAsync(Guid Id)
		{
			return Ok(await _friendsService.RemoveFriendAsync(User.Identity.Name, Id));
		}
	}
}
