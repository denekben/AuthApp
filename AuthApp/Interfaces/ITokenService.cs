using AuthApp.Models;
using AuthApp.Services;

namespace AuthApp.Interfaces {
    public interface ITokenService {
        Task<string> CreateToken(AppUser user);
        RefreshToken GenerateRefreshToken();
        public bool SetRefreshToken(AppUser user, RefreshToken newRefreshToken);
        bool Save();
    }
}
