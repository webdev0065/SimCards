using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STSSimCardProjectReactWithDotNet.Data;
using STSSimCardProjectReactWithDotNet.Models;
using STSSimCardProjectReactWithDotNet.Models.ViewModels;

namespace STSSimCardProjectReactWithDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimCardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SimCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SimCards
        [HttpGet]

        public ActionResult GetsimCards()
        {



            var result = (from simCard in _context.SimCards
                          //join ustomer in _context.Customers on simCard.CustomersId equals customer.Id
                          join provider in _context.Providers on simCard.ProviderId equals provider.Id
                          join device in _context.Devicess on simCard.DevicesId equals device.Id
                          select new
                          {
                              Number = simCard.Number,
                              IsActiveUser = simCard.IsActiveUser,
                              //FirstName = ustomer.FirstName,
                              //LastName = ustomer.LastName,
                              ProviderName = provider.Name,
                              DeviceName = device.DeviceName
                          }).ToList();

            // Assuming simCard.CustomersId is int and Customer.Id is also int
            var query = from Customer in _context.Customers
                        join simcard in _context.SimCards
                        on Customer.Id equals simcard.Id
                        select new
                        {
                            Customer.Id,
                            Customer.Name,
                            Customer.EMail,
                           
                        }; var result2 = (from simCard in _context.SimCards
                                         join provider in _context.Providers on simCard.ProviderId equals provider.Id
                                         join device in _context.Devicess on simCard.DevicesId equals device.Id
                                         join customer in _context.Customers on simCard.CustomerId equals customer.Id // Assuming the foreign key in SimCards table is CustomerId
                                         select new
                                         {
                                             Number = simCard.Number,
                                             IsActiveUser = simCard.IsActiveUser,
                                             FirstName = customer.Name,
                                             EMail = customer.EMail,
                                             ProviderName = provider.Name,
                                             DeviceName = device.DeviceName
                                         }).ToList();


            return Ok(new { result2,query,result});
        }

        // GET: api/SimCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SimCard>> GetSimCard(int id)
        {
            if (_context.SimCards == null)
            {
                return NotFound();
            }
            var simCard = await _context.SimCards.FindAsync(id);

            if (simCard == null)
            {
                return NotFound();
            }

            return simCard;
        }

        // PUT: api/SimCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSimCard(int id, SimCard simCard)
        {
            if (id != simCard.Id)
            {
                return BadRequest();
            }

            _context.Entry(simCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SimCardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SimCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SimCard>> PostSimCard([FromBody] SimCardVM simCardVM)
        {
            if (simCardVM == null) { return BadRequest("enter all data"); }
            if (_context.SimCards.Any(num => num.Number == simCardVM.SimCardNo) == true)
            {
                return
                     BadRequest("sim number already exist");
            }
            var simcard = new SimCard();
            simcard.Number = simCardVM.SimCardNo;
            simcard.CustomerId = simCardVM.CustomerId;
            simcard.Customer = _context.Customers.Find(simCardVM.CustomerId);
            simcard.IsActiveUser = simCardVM.IsActiveUser == 0 ? "1" : "0";

            simcard.ProviderId = simCardVM.ProviderId;
            
            simcard.Provider = _context.Providers.Find(simCardVM.ProviderId);
            simcard.DevicesId = simCardVM.DeviceId;
            simcard.Devices = _context.Devicess.Find(simCardVM.DeviceId);

            _context.SimCards.Add(simcard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSimCard", new { id = simcard.Id }, simcard);
        }


        // DELETE: api/SimCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSimCard(int id)
        {
            if (_context.SimCards == null)
            {
                return NotFound();
            }
            var simCard = await _context.SimCards.FindAsync(id);
            if (simCard == null)
            {
                return NotFound();
            }

            _context.SimCards.Remove(simCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SimCardExists(int id)
        {
            return (_context.SimCards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
