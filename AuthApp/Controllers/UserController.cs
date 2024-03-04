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
        private readonly RoleManager<AppUser> _roleManager;

        public UserController(UserManager<AppUser> userManager) {
            _userManager = userManager;
        }

        [HttpGet("userStatus")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserStatus() {
            // Получение текущего пользователя
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser != null) {
                // Получение ролей текущего пользователя
                var roles = await _userManager.GetRolesAsync(appUser);

                // Формирование сообщения о статусе авторизации и роли пользователя
                var message = $"Пользователь {appUser.UserName} авторизован. Роли: {string.Join(", ", roles)}";
                return Ok(message);
            }
            else {
                // Если пользователь не найден, возвращаем статус 401
                return Unauthorized("Пользователь не авторизован");
            }
        }
    }
}
