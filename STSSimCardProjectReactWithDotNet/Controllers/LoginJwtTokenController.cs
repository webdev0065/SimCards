using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using STSSimCardProjectReactWithDotNet.Data;
using STSSimCardProjectReactWithDotNet.Models;
using STSSimCardProjectReactWithDotNet.Models.ViewModels;
using STSSimCardProjectReactWithDotNet.RepositoryPattern;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace STSSimCardProjectReactWithDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginJwtTokenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public LoginJwtTokenController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("jwtauth")]
        public ActionResult IsValid([FromBody] JwtViewModel token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token.token);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return (jwtSecurityToken.ValidTo > DateTime.UtcNow) ? Ok() : NotFound();

        }

        [HttpPost]
            [Route("login")]
            public IActionResult login([FromBody] Login login)
            {
                if (login == null || string.IsNullOrEmpty(login.EMail) || string.IsNullOrEmpty(login.Password))
                {
                    return BadRequest("Please enter email and password");
                }

                var user = _context.Users.Any(n => n.EMail == login.EMail && n.Password == login.Password);
                if (user != null)
                {
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.UserData, login.Password),
                new Claim(ClaimTypes.Email, login.EMail)
               
            };

                    var token = new JwtSecurityToken(
                        claims: authClaims,
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    // Return the token in the response body
                    return Ok(new
                    {
                        token = tokenString
                    });
                }

                return Unauthorized();
            }
        }
    }
