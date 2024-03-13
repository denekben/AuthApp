using AuthApp.DTOs;
using AuthApp.Extensions;
using AuthApp.Interfaces;
using AuthApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Controllers {
    [ApiController]
    public class TokenController : Controller {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;

        public TokenController(ITokenService tokenService, UserManager<AppUser> userManager) {
            _tokenService=tokenService;
            _userManager=userManager;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var username = User.GetUsername();
            if(username==null)
                return Unauthorized("Invalid username");
            var appUser = await _userManager.FindByNameAsync(username);

            TokenDto tokenDtoToReturn;
            try {
                tokenDtoToReturn = await _tokenService.RefreshToken(appUser, tokenDto);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            HttpContext.Response.Cookies.Append("Access-Token", tokenDtoToReturn.AccessToken);

            return Ok(tokenDtoToReturn);
        }
    }
}
