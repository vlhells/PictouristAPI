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
			return await _db.Users.Include(u => u.Friends).ToListAsync();
		}

		public async Task<User> MyPageAsync(string thisUserName)
		{
			return await _db.Users.FirstOrDefaultAsync(x => x.UserName == thisUserName);
		}
	}
}
