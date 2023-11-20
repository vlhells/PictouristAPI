using PictouristAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace PictouristAPI.ViewModels
{
	public class RegisterViewModel: ViewModel
    {
		public override string Login { get; set; }

        [Required(ErrorMessage = $"{_errorMessage}")]
        [DataType(DataType.Password)]
        public override string Password { get; set; }

		[Required(ErrorMessage = $"{_errorMessage}")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		[Display(Name = "Подтвердить пароль")]
		public string PasswordConfirm { get; set; }
        [Required(ErrorMessage = $"{_errorMessage}")]
        public override string Email { get; set; }
        [Required(ErrorMessage = $"{_errorMessage}")]
        public override string Birthdate { get; set; }
    }
}
