using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.ViewModels;

namespace PictouristAPI.Services
{
	public class UsersService: IUsersService
	{
		private PictouristContext _db;

		public UsersService(PictouristContext db)
		{
			_db = db;
		}

		public async Task<IEnumerable<IndexUserViewModel>> IndexAsync()
        {
            List<IndexUserViewModel> usersForView = new List<IndexUserViewModel>();

            if (_db.Users.Count() != 0)
            {
                foreach (var user in _db.Users)
                {
                    usersForView.Add(new IndexUserViewModel(user.Id, user.UserName, user.Birthdate));
                }

                return usersForView;
            }
            //if (_db.Users.Count() != 0)
            //    return await _db.Users.Include(u => u.Friends).ToListAsync();
            return null;
		}

		public async Task<IndexUserViewModel> MyPageAsync(string thisUserName)
		{
			IndexUserViewModel result = null;
			if (thisUserName != null)
			{
				var preResult = await _db.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == thisUserName);
				result = new IndexUserViewModel(preResult.Id, preResult.UserName, preResult.Birthdate);
			}
			return result;
		}
	}
}
