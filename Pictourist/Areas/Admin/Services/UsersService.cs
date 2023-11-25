using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Areas.Admin.ViewModels;
using PictouristAPI.ViewModels;

namespace PictouristAPI.Areas.Admin.Services
{
	public class UsersService : IUsersService
	{
		private UserManager<User> _userManager;

		public UsersService(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<User> ChangePasswordAsync(string id)
		{
			User user = null;
            if (id != null)
				user = await _userManager.FindByIdAsync(id);
			return user;
		}

		public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model, IPasswordValidator<User> passwordValidator,
																IPasswordHasher<User> passwordHasher)
		{
			User user = await _userManager.FindByIdAsync(model.Id);
			if (user != null)
			{
				var _passwordValidator = passwordValidator;
				var _passwordHasher = passwordHasher;

				IdentityResult result =
					await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
				if (result.Succeeded)
				{
					user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
					await _userManager.UpdateAsync(user);
					return result;
				}
			}
			return null;
		}

		public async Task<IdentityResult> CreateAsync(CreateUserViewModel model)
		{
			User user = new User(model);
			return await _userManager.CreateAsync(user, model.Password);
		}

		public async Task<User> EditAsync(string id)
		{
			User user = null;
			if (id != null)
				user = await _userManager.FindByIdAsync(id);
			return user;
        }

		public async Task<IdentityResult> EditAsync(EditUserViewModel model)
		{
			User user = await _userManager.FindByIdAsync(model.Id);
			if (user != null)
			{
				user.Email = model.Email;
				user.UserName = model.Login;
				user.SetBirthdate(model.Birthdate);

				_userManager.UserValidators.Clear();
				var result = await _userManager.UpdateAsync(user);
				_userManager.UserValidators.Add(new MyUserValidator());
				return result;
			}
			return null;
		}

		public async Task<string> DeleteAsync(string id)
		{
			if (id != null)
			{
				User user = await _userManager.FindByIdAsync(id);
				if (user != null)
				{
					await _userManager.DeleteAsync(user);
					return "Success";
				}
			}
			return null;
		}

        public Task<IEnumerable<IndexUserViewModel>> IndexAsync()
        {
            throw new NotImplementedException();
        }
    }
}
