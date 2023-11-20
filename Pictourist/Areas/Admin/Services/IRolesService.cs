using Microsoft.AspNetCore.Identity;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Areas.Admin.ViewModels;

namespace PictouristAPI.Areas.Admin.Services
{
	public interface IRolesService
	{
		public Task<IEnumerable<IdentityRole>> IndexAsync();

		public Task<IdentityResult> CreateAsync(string name);

		public Task DeleteAsync(string id);

		public Task<IEnumerable<User>> UserList();

		public Task<ChangeRoleViewModel> EditAsync(string userId);

		public Task<User> EditAsync(string userId, List<string> roles);
	}
}
