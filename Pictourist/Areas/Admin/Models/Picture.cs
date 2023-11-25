using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PictouristAPI.Areas.Admin.Models
{
    [Table("Pictures")]
    public class Picture
	{
		[Key]
		public int Id { get; set; }
		//public string FirstLoaderGuid { get; set; }
		public string PathToFile { get; set; }
		public List<User> Users { get; set; } = new();

		public Picture()
		{
		}
	}
}

