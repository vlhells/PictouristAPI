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
			// TODO: awaits don't load datas in time.
			// TODO: stupid-handlers in all methods.
			var follower = _db.Users.Include(u => u.Friends).FirstOrDefault(f => f.NormalizedUserName == authedName.ToUpper());
			var wanted = _db.Users.Include(u => u.Friends).FirstOrDefault(w => w.Id == Id.ToString());

			if (follower != null && wanted != null & !follower.Friends.Contains(wanted))
			{
				follower.Friends.Add(wanted);

				await _db.SaveChangesAsync();

				return "Success!!!";
			}
			else if (follower.Friends.Contains(wanted))
			{
				return $"{wanted.UserName} is also in {follower.UserName} friends.";
			}

			return null;
		}

		public async Task<string> RemoveFriendAsync(string authedName, Guid Id)
		{
			var follower = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(f => f.UserName == authedName);
			var wanted = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.Id == Id.ToString());

			if (follower != null && wanted != null)
			{
				follower.Friends.Remove(wanted);

				await _db.SaveChangesAsync();

				return $"Вы успешно отписались от обновлений {wanted.UserName}";
			}

			return "Не были получены необходимые параметры: имя авторизованного пользователя, а также guid того, от кого пытаетесь подписаться.";
		}
	}
}
