using AuthApp.DTOs;
using AuthApp.Models;
using AuthApp.Services;
using Microsoft.IdentityModel.Tokens;

namespace AuthApp.Interfaces {
    public interface ITokenService {
        Task<TokenDto> CreateToken(AppUser user, bool populateExp);
        string GenerateRefreshToken();
        Task<TokenDto> RefreshToken(AppUser appUser, TokenDto tokenDto);
        bool Save();
    }
}
