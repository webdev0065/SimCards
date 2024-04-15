using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STSSimCardProjectReactWithDotNet.Models;
using STSSimCardProjectReactWithDotNet.RepositoryPattern;

namespace STSSimCardProjectReactWithDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class UserController : ControllerBase
        {

            private readonly IUserRepository _user;
            public UserController(IUserRepository userRepository)
            {
                _user = userRepository;
            }
            [HttpPost("Register")]
            public IActionResult Register(User user)
            {
                if (ModelState.IsValid)
                {
                    var IsUnique = _user.IUniqueUser(user.EMail);
                    if (!IsUnique)
                    {
                        return BadRequest("User with the provided EMail already exists");
                    }
                    var userinfo = _user.Register(user.EMail, user.Password);
                    if (userinfo == null)
                    {
                        return BadRequest("User with same Email already exists");
                    }
                    return Ok("User Register Succesfully");
                }
                return BadRequest("model state in invalid");
            }
        }
    }

