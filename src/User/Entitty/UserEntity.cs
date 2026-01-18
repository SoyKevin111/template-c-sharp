using Microsoft.AspNetCore.Identity;

namespace templatebase.src.User.Entity
{
    public class UserEntity : IdentityUser
    {
        public string Name { get; set; }
        public string CreatedAt { get; set; }
    }
}