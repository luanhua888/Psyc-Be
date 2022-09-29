using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsychologicalCounseling.Models;

//using agora.rtc;
namespace PsychologicalCounseling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly PsychologicalCouselingContext _context;
        private readonly IAgoraProvider _agoraProvider;
        // private IAgoraRtcEngine rtc_engine_ = null;

        public BookingsController(PsychologicalCouselingContext context, IAgoraProvider agoraProvider)
        {
            _context = context;
            _agoraProvider = agoraProvider;

            //     rtc_engine_ = rtc_engine;
        }

        // GET: api/Bookings
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings(string chanelname)
        {
            /*  string token = "007eJxTYJBqnMK+4b3vm+1/n3bN/eY73TquxV1dcPqvZV+mfv9/znSXAoORiWVqipFBqrFlormJuUGaZap5aoqBsWmSUVqiiYGR0emXhsne1sbJcUzWrIwMEAjiMzGEGDIwAADsxiEw";

              rtc_engine_ = AgoraRtcEngine.CreateAgoraRtcEngine();
            //  rtc_engine_ = (IAgoraRtcEngine)AgoraRtcEngine.GetEngine("249ed20e39a7470f9e7ed035b2fa4022");
              RtcEngineContext rtc_engine_context = new RtcEngineContext("249ed20e39a7470f9e7ed035b2fa4022");
              rtc_engine_.Initialize(rtc_engine_context);
              rtc_engine_.CreateChannel("T1");*/
            var token = _agoraProvider.GenerateToken(chanelname, 0.ToString(), 0);


                
            
            return Ok(new { StatusCode = 200, Message = "Load successful", data = token, chanelname= chanelname});


        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            //  var auth = agora.rtc.
            // var abc = agora.rtc.AgoraRtcEngine.CreateAgoraRtcEngine();
         
            return Ok(new { StatusCode = 200, Message = "Load successful" });
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingExists(booking.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
