using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INDWalks.API.Models.DTO;
using INDWalks.API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INDWalks.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        // POST api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.userName,
                Email = registerRequestDto.userName
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.password);

            if(identityResult.Succeeded)
            {
                //Add Roles to the user
                 identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                if(identityResult.Succeeded)
                {
                    return Ok("User registered successfully! Please proceed with Login.");
                }
            }
            return BadRequest("User Registration failed");
            
        }

        //POST api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.userName);

            if(user != null)
            {
                bool isAuthenticated = await _userManager.CheckPasswordAsync(user, loginRequestDto.password);

                if(isAuthenticated == true)
                {
                    //Get roles
                    var roles = await _userManager.GetRolesAsync(user);

                    if(roles != null)
                    {
                        //Generate JWT token
                        var token = _tokenRepository.GenerateJWTToken(user, roles.ToList());

                        LoginResponseDto loginResponse = new LoginResponseDto
                        {
                            JwtToken = token
                        };

                        return Ok(loginResponse);
                    }

                    return BadRequest("User dosen't have any role");
                }
                return BadRequest("Invalid user / Password");

            }
            return BadRequest("Invalid User");
        }

    }
}

