﻿using AuthApp.DTOs;
using AuthApp.Models;
using AuthApp.Services;
using Microsoft.IdentityModel.Tokens;

namespace AuthApp.Interfaces {
    public interface ITokenService {
        Task<TokenDto> CreateTokenDto(AppUser user, bool populateExp);
        string GenerateRefreshToken();
        Task<string> GenerateAccessToken(AppUser user);
        Task<TokenDto> RefreshToken(AppUser appUser, TokenDto tokenDto);
    }
}
