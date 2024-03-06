using Microsoft.AspNetCore.Identity;

namespace AuthApp.Models {
    public class AppUser : IdentityUser {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
