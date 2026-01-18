using templatebase.src.User.Response;

namespace templatebase.src.auth.Dtos
{
    public class UserLoginResponse
    {
        public UserResponse User { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}