using PictouristAPI.Areas.Admin.Models;

namespace PictouristAPI.Services
{
	public interface IUsersService
	{
		public Task<IEnumerable<User>> IndexAsync();
		public Task<User> MyPageAsync(string thisUserName);
	}
}
