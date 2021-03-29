using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Controllers
{
  [Route("[controller]")]
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


    [HttpPut("{id}/start")]
    public async Task<IActionResult> ChangeInterventionStatus(long id)
    {
      var findIntervention = await _context.interventions.FindAsync(id);

      if (findIntervention == null)
      {
        return NotFound();
      }

      if (findIntervention.status == "InProgress")
      {
        ModelState.AddModelError("Status", "This status is in progress.");
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      findIntervention.status = "InProgress";
      findIntervention.start_date = DateTime.Today;
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

    [HttpPut("{id}/completed")]
    public async Task<IActionResult> CompleteIntervention(long id)
    {
      var findIntervention = await _context.interventions.FindAsync(id);

      if (findIntervention == null)
      {
        return NotFound();
      }

      if (findIntervention.status == "Completed")
      {
        ModelState.AddModelError("Status", "Status completed.");
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      findIntervention.status = "Completed";
      findIntervention.end_date = DateTime.Today;

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