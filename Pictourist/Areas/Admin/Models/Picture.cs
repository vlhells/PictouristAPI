using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PictouristAPI.Areas.Admin.Models
{
    [Table("Pictures")]
    public class Picture
	{
		[Key]
		public int Id { get; set; }
		//public string FirstLoaderGuid { get; set; } // for unique picture-case. Look at PicturesService LoadPic-method.
		public string PathToFile { get; set; }
		public string PictureDescription { get; set; }

		public Picture()
		{
		}
	}
}

