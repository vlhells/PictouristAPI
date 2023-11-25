using Microsoft.AspNetCore.Mvc;
using PictouristAPI.Services;

namespace PictouristAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PicturesController : Controller
    {
        private IPicturesService _picturesService;

        public PicturesController(IPicturesService picturesService)
        {
            _picturesService = picturesService;
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

            return NoContent();
		}

        [HttpGet]
        public async Task<IActionResult> User(string userGuid) // localhost:port/Pictures/User/userGuid -- pictures of user.
        {
            var result = await _picturesService.GetUserPicturesAsync(userGuid);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

		[HttpPost]
		public async Task<IActionResult> Delete(string pictureId, string actionerGuid)
		{
			var result = await _picturesService.DeletePictureAsync(pictureId, actionerGuid);
			if (result)
			{
				return Ok("Successfully deleted picture.");
			}

			return NotFound();
		}
	}
}

