using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace identityTest.Server.Controllers
{
    
    [ApiController]
    [Produces("application/json")]
    public class UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;

        private readonly SignInManager<AppUser> _signInManager = signInManager;

        [HttpGet]
        [Route("/[controller]/getappusers")]
        public async Task<IActionResult> GetAppUsers() 
        {
            var users = await _userManager.Users.ToListAsync();
            //var serialized = JsonSerializer.Serialize(users);
            return Ok(users);
        }

        [HttpPost]
        [Route("/[controller]/login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = new();
            // passWord12+
            try 
            {
                await HttpContext.SignOutAsync(IdentityConstants.BearerScheme);

                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return BadRequest("Invalid login credentials");
                    
                }
                else 
                {
                    result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);
                    if (!result.Succeeded)
                    {
                        return BadRequest("Invalid login credentials");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong " + e.Message);
            }

            
            return Ok(new { message = "Login successful", result = result });
        }

        [HttpPost]
        [Route("/[controller]/register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            //
            // passWord12+
            await HttpContext.SignOutAsync(IdentityConstants.BearerScheme);

            IdentityResult result = new();

            try
            {
                AppUser user = new()
                {
                    Email = registerDto.Email,
                    //PasswordHash = registerDto.Password,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    UserName = registerDto.Email
                };

                var hashedPassword = new PasswordHasher<AppUser>().HashPassword(user, registerDto.Password);

                user.PasswordHash = hashedPassword;

                result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                //PassWord2+
            }
            catch (Exception e)  
            {
                return BadRequest("Something went wrong " + e.Message);
            }


            return Ok( "Registered successfully.");
        }
    }
}
