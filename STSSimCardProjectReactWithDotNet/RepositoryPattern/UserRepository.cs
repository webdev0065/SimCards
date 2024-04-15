using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using STSSimCardProjectReactWithDotNet.Data;
using STSSimCardProjectReactWithDotNet.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace STSSimCardProjectReactWithDotNet.RepositoryPattern
{
    public class UserRepository : IUserRepository
    {
            private readonly ApplicationDbContext _userManager;
            private readonly AppSetting _appSetting;

            public UserRepository(ApplicationDbContext userManager, IOptions<AppSetting> appSetting)
            {
                _userManager = userManager;
                _appSetting = appSetting.Value;
            }

            public User Authenicate(string EMail, string Password)
            {
                var UserDb = _userManager.Users.FirstOrDefault(u => u.EMail == EMail && u.Password == Password);
                if (UserDb == null) return null;
            //jwt
            var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Email,UserDb.Id.ToString()),
                new Claim(ClaimTypes.Role,UserDb.Role)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                UserDb.Token = tokenHandler.WriteToken(token);

                UserDb.Password = "";

                return UserDb;
            }
             public bool IUniqueUser(string UserName)
            {
                string EMail = null;
                var UserDb = _userManager.Logins.FirstOrDefault(u => u.EMail == EMail);
                if (UserDb == null)
                    return true;
                else
                    return false;
            }
        public User Register(string EMail, string Password)
        {
            User user = new User()
            {
                EMail = EMail,
                Password = Password,
                Role = "Individual User",
            };
            _userManager.Add(user);
            _userManager.SaveChanges();
            return user;
        }
    }
 }


