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
			if (_db.Users.Count() != 0)
				return await _db.Users.Include(u => u.Friends).
				/*Where(u => (u.Friends.Contains(authedUser) && authedUser.Friends.Contains(u)))*/
				ToListAsync();
			return null;
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
            // TODO: stupid-handlers in all methods.
            User follower = null;
            User wanted = null;
            await GetPair(follower, wanted, authedName, Id);

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

		private async Task GetPair(User follower, User wanted, string authedName, Guid Id)
		{
            using (_db)
            {
                follower = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(f => f.NormalizedUserName == authedName.ToUpper());
                wanted = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(w => w.Id == Id.ToString());
            }
        }

		public async Task<string> RemoveFriendAsync(string authedName, Guid Id)
		{
			User follower = null;
			User wanted = null;
			await GetPair(follower, wanted, authedName, Id);

			if (follower != null && wanted != null && follower.Friends.Contains(wanted))
			{
				follower.Friends.Remove(wanted);

				await _db.SaveChangesAsync();

				return $"Вы успешно отписались от обновлений {wanted.UserName}";
			}
			else if (!follower.Friends.Contains(wanted))
			{
                return $"{follower.UserName} was not sub of {follower.UserName}.";
            }

			return null;
		}
	}
}
