using Microsoft.AspNetCore.Mvc;
using PictouristAPI.Services;

namespace PictouristAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PicturesController : Controller
    {
        private IPicturesService _picturesService;
        private string _notAuthorized;


		public PicturesController(IPicturesService picturesService)
        {
            _picturesService = picturesService;
			_notAuthorized = "No access rights to perform this action or incorrect picture id.";
		}

        [HttpPost]
        public async Task<IActionResult> LoadPic(FormFileCollection files, string loaderGuid)
        {
            var result = await _picturesService.LoadPictureAsync(files, loaderGuid);
            if (result.All(r => r == false))
            {
				return BadRequest("No pictures were uploaded because of incorrect data-format.");
			}
            else if (result.All(r => r == true))
            {
				return Ok("Successfully loaded all pictures!");
			}
            else if (result.Any(r => r == true))
            {
                string preRes = "Photos num. ";
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i] == true)
                    {
                        preRes += $"{i}, ";
                    }
                }
                preRes += " were successfully loaded. Other had incorrect data.";
                return BadRequest(preRes);
            }

            return NoContent(); // this return should be never achieved.
		}

        [HttpGet]
        public async Task<IActionResult> User(string userGuid) // localhost:port/Pictures/User/userGuid -- pictures of user.
        {
            var result = await _picturesService.GetUserPicturesAsync(userGuid);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound("No user with this user id.");
        }

		[HttpPost]
		public async Task<IActionResult> Delete(string pictureId, string actionerGuid)
		{
			var result = await _picturesService.DeletePictureAsync(pictureId, actionerGuid);
			if (result)
			{
				return Ok("Successfully deleted picture.");
			}

			return NotFound(_notAuthorized);
		}

        [HttpPost]
        public async Task<IActionResult> Edit(string pictureId, string actionerId, string newDescription)
        {
            var result = await _picturesService.EditPictureDescAsync(pictureId, actionerId, newDescription);
            if (result)
            {
                return Ok("Successfully edited pictures' description");
            }
            return BadRequest(_notAuthorized);
        }
	}
}

