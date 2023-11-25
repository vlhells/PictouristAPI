using Microsoft.AspNetCore.Identity;
using PictouristAPI.Models;

namespace PictouristAPI.Areas.Admin.Models
{
    public class User : IdentityUser
    {
        //public Role Role { get; private set; }
        //public Email Email { get; private set; }

        //public Birthdate Birthdate { get; private set; }
        public string Birthdate { get; private set; }

        public List<User> Friends { get; } = new();
        public List<Picture> Pictures { get; set; } = new();

        public User()
        {

        }

        public User(ViewModel model)
        {
            Birthdate = model.Birthdate;
            Email = model.Email;
            UserName = model.Login;
        }

        public void SetBirthdate(string birthdate)
        {
            Birthdate = birthdate;
        }
    }

    //public class Birthdate
    //{
    //	public Birthdate(string date)
    //	{
    //		Date = date;
    //	}

    //	private string Date { get; set; }


    //	private Birthdate()
    //	{

    //	}
    //}
}
