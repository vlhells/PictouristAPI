using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.ViewModels;

namespace PictouristAPI.Services
{
	public interface IUsersService
	{
		public Task<IEnumerable<IndexUserViewModel>> IndexAsync();
		public Task<IndexUserViewModel> MyPageAsync(string thisUserName);
	}
}
