using System.ComponentModel.DataAnnotations;

namespace templatebase.src.auth.Dtos
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Username obligatorio.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Name obligatorio.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password obligatorio.")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}