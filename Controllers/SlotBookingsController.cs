using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsychologicalCounseling.Models;

namespace PsychologicalCounseling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SlotBookingsController : ControllerBase
    {
        private readonly PsychologicalCouselingContext _context;

        public SlotBookingsController(PsychologicalCouselingContext context)
        {
            _context = context;
        }

        // GET: api/SlotBookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SlotBooking>>> GetSlotBookings()
        {
            return await _context.SlotBookings.ToListAsync();
        }

        // GET: api/SlotBookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SlotBooking>> GetSlotBooking(int id)
        {
            var slotBooking = await _context.SlotBookings.FindAsync(id);

            if (slotBooking == null)
            {
                return NotFound();
            }

            return slotBooking;
        }

        // PUT: api/SlotBookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlotBooking(int id, SlotBooking slotBooking)
        {
            if (id != slotBooking.SlotId)
            {
                return BadRequest();
            }

            _context.Entry(slotBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotBookingExists(id))
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

        // POST: api/SlotBookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SlotBooking>> PostSlotBooking(SlotBooking slotBooking)
        {
            _context.SlotBookings.Add(slotBooking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SlotBookingExists(slotBooking.SlotId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSlotBooking", new { id = slotBooking.SlotId }, slotBooking);
        }

        // DELETE: api/SlotBookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlotBooking(int id)
        {
            var slotBooking = await _context.SlotBookings.FindAsync(id);
            if (slotBooking == null)
            {
                return NotFound();
            }

            _context.SlotBookings.Remove(slotBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SlotBookingExists(int id)
        {
            return _context.SlotBookings.Any(e => e.SlotId == id);
        }
    }
}
