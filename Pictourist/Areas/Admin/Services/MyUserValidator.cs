using Microsoft.AspNetCore.Identity;
using PictouristAPI.Areas.Admin.Models;

namespace PictouristAPI.Areas.Admin.Services
{
    public class MyUserValidator : UserValidator<User>
    {
        public override Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (user.UserName != "Admin")
            {
                if (manager.Users.Any(u => u.Email == user.Email))
                {
                    errors.Add(new IdentityError
                    {
                        Description = "Пользователь с таким Email уже зарегистрирован."
                    });
                }

                if (manager.Users.Any(u => u.UserName == user.UserName))
                {
                    errors.Add(new IdentityError
                    {
                        Description = "Пользователь с таким логином уже зарегистрирован."
                    });
                }
            }
            
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
