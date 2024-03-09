using AuthApp.DTOs;
using AuthApp.Extensions;
using AuthApp.Interfaces;
using AuthApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthApp.Controllers {
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signinManager) {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signinManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrert");

            var tokenDto = await _tokenService.CreateToken(user, populateExp: true);

            HttpContext.Response.Cookies.Append("Access-Token", tokenDto.AccessToken);
            HttpContext.Response.Cookies.Append("Username", user.UserName);
            HttpContext.Response.Cookies.Append("Refresh-Token", tokenDto.RefreshToken,
                new CookieOptions {
                    HttpOnly = true,
                    Expires = user.RefreshTokenExpires
                }
            );

            return Ok(tokenDto);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded) {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded) {

                        var tokenDto = await _tokenService.CreateToken(appUser, populateExp: true);

                        HttpContext.Response.Cookies.Append("Access-Token", tokenDto.AccessToken);
                        HttpContext.Response.Cookies.Append("Username", appUser.UserName);
                        HttpContext.Response.Cookies.Append("Refresh-Token", tokenDto.RefreshToken,
                            new CookieOptions {
                                HttpOnly = true,
                                Expires = appUser.RefreshTokenExpires
                            }
                        );

                        return Ok(tokenDto);
                    }
                    else {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else {
                    await _userManager.DeleteAsync(appUser);
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception ex) {
                return StatusCode(500, ex);
            }
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout() {
            // magic code 
            return Ok();
        }
    }
}
