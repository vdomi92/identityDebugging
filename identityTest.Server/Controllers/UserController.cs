using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace identityTest.Server.Controllers
{
    
    [ApiController]
    [Produces("application/json")]
    public class UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;

        private readonly SignInManager<AppUser> _signInManager = signInManager;

        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

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
