using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace identityTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;

        private readonly SignInManager<AppUser> _signInManager = signInManager;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAppUsers() 
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            //
            // passWord12+
            await HttpContext.SignOutAsync(IdentityConstants.BearerScheme);

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            
            if (user != null) 
            {
                var response = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);
                return Ok(response);
            }

            return Unauthorized();
        }
    }
}
