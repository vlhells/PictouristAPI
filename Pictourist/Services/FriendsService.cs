using Microsoft.EntityFrameworkCore;
using PictouristAPI.Areas.Admin.Models;

namespace PictouristAPI.Services
{
	public class FriendsService: IFriendsService
	{
		private PictouristContext _db;

		public FriendsService(PictouristContext db)
		{
			_db = db;
		}

		public async Task<IEnumerable<User>> IndexAsync()
		{
			return await _db.Users.Include(u => u.Friends).
				/*Where(u => (u.Friends.Contains(authedUser) && authedUser.Friends.Contains(u)))*/
				ToListAsync();
		}

		public async Task<User> IndexAsync(string authedName, string Id) // "Friends/Index/Friend1", etc...
		{
			var authedUser = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.UserName == authedName);

			User u = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(x => x.Id == Id);
			if (u != null && u.Friends.Contains(authedUser))
			{
				return u;
			}

			return null;
		}

		public async Task<string> AddFriendAsync(string authedName, Guid Id)
		{
			var follower = await _db.Users.FirstOrDefaultAsync(f => f.UserName == authedName);

			var wanted = await _db.Users.FirstOrDefaultAsync(u => u.Id == Id.ToString());

			follower.Friends.Add(wanted);

			await _db.SaveChangesAsync();

			return $"Вы отправили заявку на подписку на обновления {wanted.UserName}.<br>Когда пользователь её примет," +
				$"Вы сможете просматривать его фотографии.";
		}

		public async Task<string> RemoveFriendAsync(string authedName, Guid Id)
		{
			var follower = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(f => f.UserName == authedName);

			var wanted = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.Id == Id.ToString());

			follower.Friends.Remove(wanted);

			await _db.SaveChangesAsync();

			return $"Вы успешно отписались от обновлений {wanted.UserName}";
		}
	}
}
