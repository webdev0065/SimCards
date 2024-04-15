using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using STSSimCardProjectReactWithDotNet.Models;
using STSSimCardProjectReactWithDotNet.Models.ViewModels;
using STSSimCardProjectReactWithDotNet.RepositoryPattern;

namespace STSSimCardProjectReactWithDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userManager;

        public LoginController(IUserRepository userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserVM userVM)
        {
            // Authenticate user
            var userDb = _userManager.Authenicate(userVM.EMail, userVM.Password);

            // Check if userDb is null before accessing its members
            if (userDb != null)
            {
                // User authenticated, return success response
                return Ok(userDb);
            }
            else
            {
                // User not found or authentication failed, return appropriate error response
                return BadRequest("WrongUser/Password");
            }
        }

    }

}



