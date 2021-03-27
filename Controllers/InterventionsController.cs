using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace Rocket_Elevators_REST_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InterventionsController : ControllerBase
  {
    private readonly RestAPIContext _context;

    public InterventionsController(RestAPIContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Intervention>>> GetBatteries()
    {
      return await _context.interventions.ToListAsync();
    }

    [HttpGet("pending")]
    public async Task<ActionResult<List<Intervention>>> Pending()
    {
      var interventions = await _context.interventions
        .Where(intervention => intervention.start_date == null && intervention.status == "Pending")
        .ToListAsync();

      if (interventions == null)
      {
        return NotFound();
      }

      return interventions;
    }


   // PUT: api/Interventions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/inprogress")]
        public async Task<IActionResult> PutIntervention(long id, Intervention intervention)
        {
            if (id != intervention.id)
            {
                return BadRequest();
            }

            Intervention interventionFound = await _context.interventions.FindAsync(id);
            interventionFound.status = intervention.status;
            interventionFound.start_date = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
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

    // PUT: api/Interventions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}/completed")]
        public async Task<IActionResult> PutIntervention2(long id, Intervention intervention)
        {
            if (id != intervention.id)
            {
                return BadRequest();
            }

            Intervention interventionFound = await _context.interventions.FindAsync(id);
            interventionFound.status = intervention.status;
            interventionFound.end_date = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
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
    


         private bool InterventionExists(long id)
        {
            return _context.interventions.Any(e => e.id == id);
        }

        }
  }
  





