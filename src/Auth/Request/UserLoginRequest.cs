using System.ComponentModel.DataAnnotations;

namespace templatebase.src.Auth.Request
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Username obligatorio.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password obligatorio.")]
        public string Password { get; set; }
    }
}