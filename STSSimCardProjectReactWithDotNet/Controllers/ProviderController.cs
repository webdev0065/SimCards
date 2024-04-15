using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STSSimCardProjectReactWithDotNet.Data;
using STSSimCardProjectReactWithDotNet.Models;

namespace STSSimCardProjectReactWithDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProviderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
      {
            var data = await _context.Providers.ToListAsync();
            return Ok(data);
        }

        [HttpPost("Users")]
        public async Task<IActionResult> Provider(Provider newProvider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var providerToAdd = new Provider
            {
                Name = newProvider.Name,
                // Assign other properties as needed
            };

            var result = await _context.Providers.AddAsync(providerToAdd);
            await _context.SaveChangesAsync();

            if (result != null)
            {
                return Ok(new { Message = "Provider Successfully" });
            }
            else
            {
                return BadRequest("Provider wrong");
            }
        }
        [HttpPut("{id}")] // Added id parameter to route
        public IActionResult UpdateProvider(int id, [FromBody] Provider provider)
        {
            var user = _context.Providers.Find(id);
            if (user == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /*      Devices device = new Devices(); */// Create an instance of the Devices class
            user.Name = provider.Name;
            _context.Providers.Update(provider);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")] // Added id parameter to route
        public IActionResult DeleteDevice(int id)
        {
            var UserInDb = _context.Devicess.Find(id);
            if (UserInDb == null)
                return NotFound();

            _context.Devicess.Remove(UserInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}



