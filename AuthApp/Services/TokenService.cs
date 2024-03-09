using AuthApp.Data;
using AuthApp.DTOs;
using AuthApp.Interfaces;
using AuthApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthApp.Services {
    public class TokenService : ITokenService {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager, AppDbContext context) {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            _userManager = userManager;
            _context = context;
        }
        public async Task<TokenDto> CreateToken(AppUser user, bool populateExp) {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles) {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            string accessToken = tokenHandler.WriteToken(token);

            
            if (populateExp) {
                var refreshToken = GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpires = DateTime.Now.AddDays(7);
            }

            await _userManager.UpdateAsync(user);
            Save();

            var tokenDto = new TokenDto {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken
            };

            return tokenDto;
        }

        public string GenerateRefreshToken() {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public async Task<TokenDto> RefreshToken(AppUser appUser, TokenDto tokenDto) {
            if (appUser == null || appUser.RefreshToken != tokenDto.RefreshToken ||
                appUser.RefreshTokenExpires <= DateTime.Now)
                throw new Exception("Invalid client request.The tokenDto has some invalid values.");

            return await CreateToken(appUser,populateExp: false);
        }

        public bool Save() {
            return Convert.ToBoolean(_context.SaveChanges());
        }
    }
}
