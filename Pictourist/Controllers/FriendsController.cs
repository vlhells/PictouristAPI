using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Services;

namespace PictouristAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	//[Authorize]
	public class FriendsController : ControllerBase
	{
		private IFriendsService _friendsService;

		public FriendsController(IFriendsService friendsService)
		{
			_friendsService = friendsService;
		}

		[HttpGet]
		public async Task<IActionResult> IndexAsync(string authedUserName, string? Id) // "Friends/Index/Friend1", etc...
		{
			if (Id == null)
			{
				return new ObjectResult(await _friendsService.IndexAsync());
			}
			else
			{
				User u = await _friendsService.IndexAsync(authedUserName, Id);
				if (u != null)
				{
					return new ObjectResult(u);
				}
			}

			return NoContent();
		}

		[HttpPost]
		public async Task<IActionResult> AddFriendAsync(string authedUserName, Guid Id)
		{
			var result = await _friendsService.AddFriendAsync(authedUserName, Id);

			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest("Не были переданы необходимые параметры: guid желаемого и (или) имя отправителя запроса.");
			}
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFriendAsync(Guid Id, string authedUserName)
		{
			return Ok(await _friendsService.RemoveFriendAsync(authedUserName, Id));
		}
	}
}
