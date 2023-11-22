using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.Areas.Admin.ViewModels;

namespace PictouristAPI.Areas.Admin.Services
{
	public class RolesService: IRolesService
	{
		private RoleManager<IdentityRole> _roleManager;
		private UserManager<User> _userManager;

		public RolesService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}

		public async Task<IEnumerable<IdentityRole>> IndexAsync()
		{
			if (_roleManager.Roles.Count() != 0)
				return await _roleManager.Roles.ToListAsync();
			return null;
		}

		public async Task<IdentityResult> CreateAsync(string name)
		{
			IdentityResult result = null;
			if (!string.IsNullOrEmpty(name))
			{
				result = await _roleManager.CreateAsync(new IdentityRole(name));
			}
			return result;
		}

		public async Task<string> DeleteAsync(string id)
		{
			if (id != null)
			{
				IdentityRole role = await _roleManager.FindByIdAsync(id);
				if (role != null)
				{
					await _roleManager.DeleteAsync(role);
                    return "Success";
                }
			}
			return null;
		}

		public async Task<IEnumerable<User>> UserList()
		{
			if (_userManager.Users.Count() != 0)
				return await _userManager.Users.ToListAsync();
			return null;
		}

		public async Task<ChangeRoleViewModel> EditAsync(string userId)
		{
			User user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				var userRoles = await _userManager.GetRolesAsync(user);
				var allRoles = _roleManager.Roles.ToList();
				ChangeRoleViewModel model = new ChangeRoleViewModel
				{
					UserId = user.Id,
					UserEmail = user.Email,
					UserRoles = userRoles,
					AllRoles = allRoles
				};
				return model;
			}

			return null;
		}

		public async Task<User> EditAsync(string userId, List<string> roles)
		{
			User user = null;

			if (userId != null)
			{
				user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var allRoles = _roleManager.Roles.ToList();
                    var addedRoles = roles.Except(userRoles);
                    var removedRoles = userRoles.Except(roles);

                    await _userManager.AddToRolesAsync(user, addedRoles);

                    await _userManager.RemoveFromRolesAsync(user, removedRoles);

                    return user;
                }
            }

			return null;
		}
	}
}
