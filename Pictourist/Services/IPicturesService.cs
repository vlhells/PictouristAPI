using System;
namespace PictouristAPI.Services
{
	public interface IPicturesService
	{
		public Task<List<bool>> LoadPicture(FormFileCollection files, string loaderGuid);
	}
}

