using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModel
{
    public class UserModel
    {
        [Required(ErrorMessage = "Informe um nome")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Informe um password")]
        public string Password { get; set; }
        public string EmailAddress { get; set; }
    }
}
