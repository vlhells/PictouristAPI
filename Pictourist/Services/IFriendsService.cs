using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.ViewModels;

namespace PictouristAPI.Services
{
	public interface IFriendsService
	{
		public Task<IEnumerable<IndexUserViewModel>> IndexAsync();

		public Task<IndexUserViewModel> IndexAsync(string authedName, string Id);

		public Task<string> AddFriendAsync(string authedName, Guid Id);

		public Task<string> RemoveFriendAsync(string authedName, Guid Id);
	}
}
