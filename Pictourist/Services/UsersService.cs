using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PictouristAPI.Areas.Admin.Models;

namespace PictouristAPI.Services
{
	public class UsersService: IUsersService
	{
		private PictouristContext _db;

		public UsersService(PictouristContext db)
		{
			_db = db;
		}

		public async Task<IEnumerable<User>> IndexAsync()
        {
            if (_db.Users.Count() != 0)
                return await _db.Users.Include(u => u.Friends).ToListAsync();
            return null;
		}

		public async Task<User> MyPageAsync(string thisUserName)
		{
			User result = null;
			if (thisUserName != null)
			{
				result = await _db.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == thisUserName);
			}
			return result;
		}
	}
}
