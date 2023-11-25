using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Services;
using PictouristAPI.ViewModels;

namespace PictouristAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	//[Authorize]
	public class FriendsController : ControllerBase
	{
		private IFriendsService _friendsService;
		private string _badReqGuidName;

        public FriendsController(IFriendsService friendsService)
		{
			_friendsService = friendsService;
			_badReqGuidName = "Input params error.";
        }

		[HttpGet]
		public async Task<IActionResult> IndexAsync(string authedUserName, string? Id) // "Friends/Index/Friend1", etc...
		{
			if (Id == null)
			{
				var result = await _friendsService.IndexAsync();
				if (result != null)
					return new ObjectResult(result);
			}
			else
			{
                IndexUserViewModel u = await _friendsService.IndexAsync(authedUserName, Id);
				if (u != null)
				{
					return new ObjectResult(u);
				}
			}

			return NotFound("No users exists in db.");
		}

		[HttpPost]
		public async Task<IActionResult> AddFriendAsync(string authedUserName, Guid Id)
		{
            var result = await _friendsService.AddFriendAsync(authedUserName, Id);

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(_badReqGuidName);
        }

		[HttpPost]
		public async Task<IActionResult> RemoveFriendAsync(string authedUserName, Guid Id)
		{
			var result = await _friendsService.RemoveFriendAsync(authedUserName, Id);
            if (result != null)
			{
				return Ok(result);
			}
            return BadRequest(_badReqGuidName);
        }
	}
}
