using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STSSimCardProjectReactWithDotNet.Data;
using STSSimCardProjectReactWithDotNet.Models;
using System.Threading.Tasks;

namespace STSSimCardProjectReactWithDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var data = await _context.Registers.ToListAsync();
            return Ok(data);
        }

        [HttpPost("Users")] // Corrected route for the Users action
        public async Task<IActionResult> Users(Register model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new Register
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EMail = model.EMail,
                PhoneNumber = model.PhoneNumber
            };
            var result = await _context.Registers.AddAsync(user);
            await _context.SaveChangesAsync();

            if (result != null)
            {
                return Ok(new { Message = "User registered Successfully" });
            }
            else
            {
                return BadRequest("User Registered getting error");
            }
        }

        [HttpPut("{id}")] // Added id parameter to route
        public IActionResult UpdateUsers(int id, [FromBody] Register Registers)
        {
            var user = _context.Registers.Find(id);
            if (user == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            user.FirstName = Registers.FirstName;
            user.LastName = Registers.LastName;
            user.EMail = Registers.EMail;
            user.PhoneNumber = Registers.PhoneNumber;

            _context.Registers.Update(user);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")] // Added id parameter to route
        public IActionResult DeleteUser(int id)
        {
            var UserInDb = _context.Registers.Find(id);
            if (UserInDb == null)
                return NotFound();

            _context.Registers.Remove(UserInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
