using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var result = await _picturesService.LoadPicture(files, loaderGuid);
            if (result.All(r => r == false))
            {
				return BadRequest("No pictures were uploaded because of incorrect data-format.");
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

			return Ok("Successfully loaded all pictures!");
		}
    }
}

