using AuthApp.Models;

namespace AuthApp.Interfaces {
    public interface ITokenService {
        string CreateToken(AppUser user);
    }
}
