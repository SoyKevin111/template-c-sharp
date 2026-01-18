using templatebase.src.User.Response;

namespace templatebase.src.Auth.Response
{
    public class UserLoginResponse
    {
        public UserResponse User { get; set; }
        public string Token { get; set; }
    }
}