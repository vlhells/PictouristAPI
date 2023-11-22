using Microsoft.AspNetCore.Identity;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Areas.Admin.ViewModels;
using PictouristAPI.ViewModels;

namespace PictouristAPI.Areas.Admin.Services
{
	public interface IRolesService
	{
		public Task<IEnumerable<IdentityRole>> IndexAsync();

		public Task<IdentityResult> CreateAsync(string name);

		public Task<string> DeleteAsync(string id);

		public Task<IEnumerable<IndexUserViewModel>> UserList();

		public Task<ChangeRoleViewModel> EditAsync(string userId);

		public Task<IndexUserViewModel> EditAsync(string userId, List<string> roles);
	}
}
