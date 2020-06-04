using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.Dto.Authentication;
using IAuthenticationService = QuizApp.BLL.Interfaces.IAuthenticationService;

namespace QuizApp.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;


        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }


        [HttpGet]
        public ActionResult<UserLoggedinDto> GetCurrentUser()
        {
            return Ok(authenticationService.GetUserByEmail(User.Identity.Name));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisteredDto>> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null)
            {
                return BadRequest();
            }

            if (!authenticationService.TryRegisterUser(userRegisterDto, out var userRegisteredDto))
            {
                var result = new
                {
                    Message = "Validation errors",
                    Errors = new List<string> { "User with such email already exists." }
                };

                return new BadRequestObjectResult(result);
            }

            await Authenticate(userRegisteredDto.Email);

            return Ok(userRegisteredDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoggedinDto>> Login([FromBody] UserLoginDto userLoginDto)
        {
            UserLoggedinDto userLoggedin;
            if (User.Identity.IsAuthenticated)
            {
                userLoggedin = authenticationService.GetUserByEmail(User.Identity.Name);
            }
            else
            {
                if (userLoginDto == null)
                {
                    return BadRequest();
                }

                userLoggedin = authenticationService.GetUser(userLoginDto);
                if (userLoggedin == null)
                {
                    var result = new
                    {
                        Message = "Validation errors",
                        Errors = new List<string> { "Incorrect email or password." }
                    };

                    return new BadRequestObjectResult(result);
                }

                await Authenticate(userLoginDto.Email);
            }

            return Ok(userLoggedin);
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, userName) };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            var authenticationProperties = new AuthenticationProperties { IsPersistent = true };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id), authenticationProperties);
        }
    }
}
