using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiOrder_master.Dto;
// using ApiOrder_master.Services;
using ApiWeb_master.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

namespace ApiOrder_master.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        // private IUserService _userService;

        private readonly IConfiguration _config;
        

        public UsersController(IConfiguration config)
        {
            //_userService = userService;
            _config = config;
            
        }

        // [HttpPost("authenticate")]
        // public IActionResult Authenticate(AuthenticateRequest model)
        // {
        //     var response = _userService.Authenticate(model);

        //     if (response == null)
        //         return BadRequest(new { message = "Username or password is incorrect" });

        //     return Ok(response);
        // }

        // [Authorize]
        // [HttpGet]
        // public IActionResult GetAll()
        // {
        //     var users = _userService.GetAll();
        //     return Ok(users);
        // }
        // [HttpPost("register")]
        // public async Task<IActionResult> Register(AuthenticateRequest authenticateRequest)
        // {
        //     authenticateRequest.Username = authenticateRequest.Username.ToLower();

        //     // if(await _repo.UserExists(authenticateRequest.Username))
        //     // return BadRequest ("Username already exists");

        //     var userToCreate = new User
        //     {
        //     Username = authenticateRequest.Username
        //     };

        //     var createdUser = await _repo.Register(userToCreate, authenticateRequest.Password);
        //     return StatusCode(201);
        // }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            // var userDFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            // if(userDFromRepo == null)
            // return Unauthorized();

            var claims = new[]
            {
                // new Claim(ClaimTypes.NameIdentifier, userDFromRepo.Id.ToString()),
                // new Claim(ClaimTypes.Name, userDFromRepo.Username)
                new Claim(ClaimTypes.NameIdentifier,"1"),
                new Claim(ClaimTypes.Name, "john")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });


        }
    }
}