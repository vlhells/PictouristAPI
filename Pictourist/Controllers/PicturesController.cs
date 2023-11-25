using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> LoadPic(FormFileCollection files)
        {
            var result = await _picturesService.LoadPicture(files);
            if (result)
            {
                return Ok("Successfully loaded pictures!");
            }

            return BadRequest("Some troubles");
        }
    }
}

