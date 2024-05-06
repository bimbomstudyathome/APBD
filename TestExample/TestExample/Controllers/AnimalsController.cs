using Microsoft.AspNetCore.Mvc;
using TestExample.Repositories;

namespace TestExample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private IAnimalsRepository _animalsRepository;

    public AnimalsController(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }
    
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetAnimal(int Id)
    {
        if (!await _animalsRepository.AnimalExists(Id))
        {
            return NotFound("Animal with a given ID is not found");
        }

        var AnimalData = await _animalsRepository.AnimalById(Id);
        
        return Ok(AnimalData);
    }
}