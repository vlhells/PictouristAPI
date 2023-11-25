using System;
namespace PictouristAPI.Services
{
	public interface IPicturesService
	{
		public Task<bool> LoadPicture(FormFileCollection files, string loaderGuid);
	}
}

