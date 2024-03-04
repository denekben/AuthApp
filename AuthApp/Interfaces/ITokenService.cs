using AuthApp.Models;

namespace AuthApp.Interfaces {
    public interface ITokenService {
        Task<string> CreateToken(AppUser user);
    }
}
