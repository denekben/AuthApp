using AuthApp.Extensions;
using AuthApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthApp.Controllers {
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager) {
            _userManager = userManager;
        }

        [HttpGet("userStatus")]
        public async Task<IActionResult> GetUserStatus() {
            // Получение текущего пользователя
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser != null) {
                // Получение ролей текущего пользователя
                var roles = await _userManager.GetRolesAsync(appUser);

                // Формирование сообщения о статусе авторизации и роли пользователя
                var message = $"User {appUser.UserName} is authorized. Roles: {string.Join(", ", roles)}";
                return Ok(message);
            }
            else {
                return Ok("User is unauthorized");
            }
        }
    }
}
