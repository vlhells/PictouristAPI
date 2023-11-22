using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PictouristAPI.ViewModels
{
	public class IndexUserViewModel
	{
		public string Guid { get; set; }
        public string Login { get; set; }
        public int Age { get; set; }

        public IndexUserViewModel(string Id, string Login, string Birthdate)
		{
			Guid = Id;
			this.Login = Login;
			string[] bdParts = Birthdate.Split('-');
			Age = DateTime.Now.Year - int.Parse(bdParts[0]);
		}
	}
}

