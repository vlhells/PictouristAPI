using Microsoft.AspNetCore.Identity;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Areas.Admin.ViewModels;

namespace PictouristAPI.Areas.Admin.Services
{
	public interface IUsersService
	{
		public Task<IEnumerable<User>> IndexAsync();
		public Task<User> ChangePasswordAsync(string id);
		public Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model, IPasswordValidator<User> passwordValidator,
																IPasswordHasher<User> passwordHasher);
		public Task<IdentityResult> CreateAsync(CreateUserViewModel model);
		public Task<User> EditAsync(string id);
		public Task<IdentityResult> EditAsync(EditUserViewModel model);
		public Task<string> DeleteAsync(string id);
	}
}
