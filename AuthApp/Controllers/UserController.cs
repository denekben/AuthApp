using AuthApp.Extensions;
using AuthApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Controllers {
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("userStatus")]
        [Authorize]
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
