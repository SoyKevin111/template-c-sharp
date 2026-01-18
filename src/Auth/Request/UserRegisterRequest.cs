using System.ComponentModel.DataAnnotations;

namespace templatebase.src.Auth.Request
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "Username obligatorio.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Name obligatorio.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password obligatorio.")]
        public string Password { get; set; }
    }
}