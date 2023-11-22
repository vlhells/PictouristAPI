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

        public IndexUserViewModel(Guid Id, string Login, string Birthdate)
		{
			Guid = Id.ToString();
			this.Login = Login;
			Age = ;
		}
	}
}

