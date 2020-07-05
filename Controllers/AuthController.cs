using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister)
        {
            //validate request

            userForRegister.Username = userForRegister.Username.ToLower();

            if (await _authRepository.UserExists( userForRegister.Username))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = new User
            {
                Username =  userForRegister.Username
            };

            User user = await _authRepository.Register(userToCreate,  userForRegister.Password);

            return StatusCode(201);
        }


    }
}