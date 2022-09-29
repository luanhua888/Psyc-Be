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
    public class SpecializationTypesController : ControllerBase
    {
        private readonly PsychologicalCouselingContext _context;

        public SpecializationTypesController(PsychologicalCouselingContext context)
        {
            _context = context;
        }

        // GET: api/SpecializationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecializationType>>> GetSpecializationTypes()
        {
            return await _context.SpecializationTypes.ToListAsync();
        }

        // GET: api/SpecializationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecializationType>> GetSpecializationType(int id)
        {
            var specializationType = await _context.SpecializationTypes.FindAsync(id);

            if (specializationType == null)
            {
                return NotFound();
            }

            return specializationType;
        }

        // PUT: api/SpecializationTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecializationType(int id, SpecializationType specializationType)
        {
            if (id != specializationType.Id)
            {
                return BadRequest();
            }

            _context.Entry(specializationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecializationTypeExists(id))
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

        // POST: api/SpecializationTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpecializationType>> PostSpecializationType(SpecializationType specializationType)
        {
            _context.SpecializationTypes.Add(specializationType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SpecializationTypeExists(specializationType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSpecializationType", new { id = specializationType.Id }, specializationType);
        }

        // DELETE: api/SpecializationTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecializationType(int id)
        {
            var specializationType = await _context.SpecializationTypes.FindAsync(id);
            if (specializationType == null)
            {
                return NotFound();
            }

            _context.SpecializationTypes.Remove(specializationType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecializationTypeExists(int id)
        {
            return _context.SpecializationTypes.Any(e => e.Id == id);
        }
    }
}
