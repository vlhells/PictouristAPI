using Microsoft.AspNetCore.Identity;
using PictouristAPI.Models;

namespace PictouristAPI.Areas.Admin.Models
{
    public class User : IdentityUser
    {
        //public int Id { get; private set; }
        //public Role Role { get; private set; }
        //public string Login { get; private set; }
        //public Email Email { get; private set; }
        //public string Password { get; private set; }

        //public Birthdate Birthdate { get; private set; }
        public string Birthdate { get; private set; }

        public List<User> Friends { get; } = new();

        //public List<string> PicturesPaths { get; } = new List<string>();

        //private List<string> _friends;

        //public string Friends
        //{
        //	get
        //	{
        //              return String.Join(';', _friends);
        //          }
        //	private set
        //	{

        //          }
        //}

        //private List<string> _userPhotos;

        //public string UserPhotos
        //{
        //	get
        //	{
        //              return String.Join(';', _userPhotos);
        //          }
        //	private set
        //	{

        //	}
        //}

        //public void AddFriend(string id)
        //{
        //          _friends = id.Split(';').ToList();
        //      }

        //public void AddPhoto()
        //{
        //          _userPhotos = value.Split(';').ToList();
        //      }

        public User()
        {

        }

        public User(ViewModel model)
        {
            //_userPhotos = new List<string>();
            //_friends = new List<string>();
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
