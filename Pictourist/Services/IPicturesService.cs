using PictouristAPI.ViewModels;
using System;
namespace PictouristAPI.Services
{
	public interface IPicturesService
	{
		public Task<List<bool>> LoadPictureAsync(FormFileCollection files, string loaderGuid);
		public Task<IndexUserViewModel> GetUserPicturesAsync(string userGuid);
		public Task<bool> DeletePictureAsync(string pictureId, string actionerGuid);
	}
}

