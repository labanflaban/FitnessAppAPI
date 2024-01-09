using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FittnessAppAPI.Data;
using FittnessAppAPI.Models;

namespace FitnessAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcerciseDaysController : ControllerBase
    {
        private readonly FitnessDbContext _context;

        public ExcerciseDaysController(FitnessDbContext context)
        {
            _context = context;
        }

        // GET: api/ExcerciseDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExcerciseDay>>> GetExcerciseDays()
        {
          if (_context.ExcerciseDays == null)
          {
              return NotFound();
          }
            return await _context.ExcerciseDays.ToListAsync();
        }

        // GET: api/ExcerciseDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExcerciseDay>> GetExcerciseDay(int id)
        {
          if (_context.ExcerciseDays == null)
          {
              return NotFound();
          }
            var excerciseDay = await _context.ExcerciseDays.FindAsync(id);

            if (excerciseDay == null)
            {
                return NotFound();
            }

            return excerciseDay;
        }

        // PUT: api/ExcerciseDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExcerciseDay(int id, ExcerciseDay excerciseDay)
        {
            if (id != excerciseDay.Id)
            {
                return BadRequest();
            }

            _context.Entry(excerciseDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExcerciseDayExists(id))
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

        // POST: api/ExcerciseDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExcerciseDay>> PostExcerciseDay(ExcerciseDay excerciseDay)
        {
          if (_context.ExcerciseDays == null)
          {
              return Problem("Entity set 'FitnessDbContext.ExcerciseDays'  is null.");
          }
            _context.ExcerciseDays.Add(excerciseDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExcerciseDay", new { id = excerciseDay.Id }, excerciseDay);
        }

        // DELETE: api/ExcerciseDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExcerciseDay(int id)
        {
            if (_context.ExcerciseDays == null)
            {
                return NotFound();
            }
            var excerciseDay = await _context.ExcerciseDays.FindAsync(id);
            if (excerciseDay == null)
            {
                return NotFound();
            }

            _context.ExcerciseDays.Remove(excerciseDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExcerciseDayExists(int id)
        {
            return (_context.ExcerciseDays?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
