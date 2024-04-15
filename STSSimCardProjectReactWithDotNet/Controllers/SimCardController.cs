//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using STSSimCardProjectReactWithDotNet.Data;
//using STSSimCardProjectReactWithDotNet.Models;
//using STSSimCardProjectReactWithDotNet.Models.ViewModels;

//namespace STSSimCardProjectReactWithDotNet.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SimCardController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public SimCardController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        [Authorize]
//        public ActionResult GetsimCards()
//        {


//            var result = (from simCard in _context.simCards
//                          join userModel in _context.userModels on simCard.UserId equals userModel.Id
//                          join provider in _context.providers on simCard.ProviderId equals provider.Id
//                          join device in _context.devices on simCard.DeviceId equals device.Id
//                          select new
//                          {
//                              simCard.Number,
//                              simCard.IsActiveUser,
//                              userModel.FirstName,
//                              userModel.LastName,
//                              provider.ProviderName,
//                              device.DeviceName

//                          }).ToList();
//            var result = (from simCard in _context.SimCards
//                          join customer in _context.Customers on simCard.CustomersId equals customer.Id
//                          join provider in _context.Providers on simCard.ProviderId equals provider.Id
//                          join device in _context.Devicess on simCard.DevicesId equals device.Id
//                          select new
//                          {
//                              Number = simCard.Number,
//                              IsActiveUser = simCard.IsActiveUser,
//                              FirstName = simCard.FirstName,
//                              LastName = simCard.LastName,
//                              ProviderName = provider.ProviderName,
//                              DeviceName = device.DeviceName
//                          }).ToList();


//            return Ok(result);
//        }

//        GET: api/SimCards/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<SimCard>> GetSimCard(int id)
//        {
//            var simCard = await _context.SimCards.FindAsync(id);

//            if (simCard == null)
//            {
//                return NotFound();
//            }

//            return simCard;
//        }

//        PUT: api/SimCards/5
//         To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutSimCard(int id, SimCard simCard)
//        {
//            if (id != simCard.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(simCard).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!SimCardExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }

//            }

//            return NoContent();
//        }

//        POST: api/SimCards
//        To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<SimCard>> PostSimCard([FromBody] SimCardVM simCardVM)
//        {
//            if (simCardVM == null) { return BadRequest("enter all data"); }
//            if (_context.SimCards.Any(num => num.Number == simCardVM.SimCardNo) == true)
//            {
//                return
//                     BadRequest("sim number already exist");
//            }
//            var simcard = new SimCard();
//            simcard.Number = simCardVM.SimCardNo;
//            simcard.CustomersId = simCardVM.CustomerId;
//            simcard.IsActiveUser = simCardVM.IsActiveUser == 0 ? "1" : "0";

//            simcard.ProviderId = simCardVM.ProviderId;
//            simcard.Customer = _context.Customers.Find(simCardVM.CustomerId);
//            simcard. = _context.Providers.Find(simCardVM.ProviderId);
//            simcard.Devices = _context.Devicess.Find(simCardVM.DeviceId);

//            _context.SimCards.Add(simcard);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetSimCard", new { id = simcard.Id }, simcard);
//        }

//        DELETE: api/SimCards/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteSimCard(int id)
//        {
//            var simCard = await _context.SimCards.FindAsync(id);
//            if (simCard == null)
//            {
//                return NotFound();
//            }

//            _context.SimCards.Remove(simCard);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool SimCardExists(int id)
//        {
//            return _context.Simc.Any(e => e.Id == id);
//        }
//    }
//}