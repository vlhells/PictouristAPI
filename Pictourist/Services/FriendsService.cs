﻿using Microsoft.EntityFrameworkCore;
using PictouristAPI.Areas.Admin.Models;
using PictouristAPI.ViewModels;

namespace PictouristAPI.Services
{
	public class FriendsService: IFriendsService
	{
		private PictouristContext _db;

		public FriendsService(PictouristContext db)
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
					usersForView.Add(new IndexUserViewModel(user.Id, user.UserName, user.Birthdate, user.Pictures));
				}

				return usersForView;

				//return await _db.Users.Include(u => u.Friends).
				///*Where(u => (u.Friends.Contains(authedUser) && authedUser.Friends.Contains(u)))*/
				//ToListAsync();


			}

			return null;
		}

		public async Task<IndexUserViewModel> IndexAsync(string authedName, string Id) // "Friends/Index/Friend1", etc...
		{
			User authedUser = null;
			User u = null;

			authedUser = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.UserName == authedName);

			u = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(x => x.Id == Id);
			if (u != null && u.Friends.Contains(authedUser))
			{
				return new IndexUserViewModel(u.Id, u.UserName, u.Birthdate, u.Pictures);
			}

			return null;
		}

		public async Task<string> AddFriendAsync(string authedName, Guid Id)
		{
			if (Id != null && authedName != null)
			{
				User follower = null;
				User wanted = null;
				(follower, wanted) = await GetPair(authedName, Id);

				if (follower != null && wanted != null & !follower.Friends.Contains(wanted) && follower.Id != wanted.Id)
				{
					follower.Friends.Add(wanted);

					await _db.SaveChangesAsync();

					return "Success!!!";
				}
				else if (follower.Friends.Contains(wanted))
				{
					return $"{wanted.UserName} is also in {follower.UserName} friends.";
				}
			}

			return null;
		}

		private async Task<(User follower, User wanted)> GetPair(string authedName, Guid Id)
		{
			User follower = null;
			User wanted = null;
            using (_db)
            {
                follower = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(f => f.NormalizedUserName == authedName.ToUpper());
                wanted = await _db.Users.Include(u => u.Friends).FirstOrDefaultAsync(w => w.Id == Id.ToString());
            }
			return (follower, wanted);
        }

		public async Task<string> RemoveFriendAsync(string authedName, Guid Id)
		{
			if (Id != null && authedName != null)
			{
				User follower = null;
				User wanted = null;
				(follower, wanted) = await GetPair(authedName, Id);

				if (follower != null && wanted != null && follower.Friends.Contains(wanted) && follower.Id != wanted.Id)
				{
					follower.Friends.Remove(wanted);

					await _db.SaveChangesAsync();

					return $"Вы успешно отписались от обновлений {wanted.UserName}";
				}
				else if (!follower.Friends.Contains(wanted))
				{
					return $"{follower.UserName} was not sub of {follower.UserName}.";
				}
			}

			return null;
		}
	}
}
