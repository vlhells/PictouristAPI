using PictouristAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace PictouristAPI.Areas.Admin.ViewModels
{
    public class EditUserViewModel : ViewModel
    {
        public string Id { get; set; }

        public override string Login { get; set; }

        [Required]
        public override string Email { get; set; }

        [Required]
        public override string Birthdate { get; set; }
    }
}
