using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STSSimCardProjectReactWithDotNet.Data;
using STSSimCardProjectReactWithDotNet.Models;

namespace STSSimCardProjectReactWithDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
            private readonly ApplicationDbContext _context;

            public DeviceController(ApplicationDbContext context)
            {
                _context = context;
            }
        
            [HttpGet]
            public async Task<IActionResult> GetAllUser()
            {
                var data = await _context.Devicess.ToListAsync();
                return Ok(data);
            }

            [HttpPost("Users")] // Corrected route for the Users action
            public async Task<IActionResult> Device(Devices model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var devices = new Devices
                {
                    DeviceName = model.DeviceName,


                };
                var result = await _context.Devicess.AddAsync(devices);
                await _context.SaveChangesAsync();

                if (result != null)
                {
                    return Ok(new { Message = "Device Successfully" });
                }
                else
                {
                    return BadRequest("Device wrong");
                }
            }

            [HttpPut("{id}")] // Added id parameter to route
            public IActionResult UpdateDevice(int id, [FromBody] Devices devices)
            {
                var user = _context.Devicess.Find(id);
                if (user == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

      /*      Devices device = new Devices(); */// Create an instance of the Devices class
            user.DeviceName = devices.DeviceName;
            _context.Devicess.Update(user);
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

