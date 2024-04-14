using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controller;
[ApiController]
[Route("[controller]")]

public class VisitController : ControllerBase
{
    private static List<Visit> visits = new List<Visit>();
    // Retrieve list of visits
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(visits);
    }
    
    //retrieve a list of visits associated with a given animal

    [HttpGet("{animalId}")]
    public IActionResult GetVisitByAnimalId(Guid animalId)
    {
        List<Visit> associatedVistis = new List<Visit>();
        foreach (var visit in visits)
        {
            if(visit.Animal.Id == animalId) associatedVistis.Add(visit);
        }
        if (associatedVistis.Count == 0) return NotFound();
        return Ok(associatedVistis);
    }

    [HttpPost]
    public IActionResult AddVisit(Visit visit)
    {
        visits.Add(visit);
        return CreatedAtAction(nameof(Get), new { id = visit.Id }, visit);
    }

}