using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task8.Context;
using Task8.Models;

namespace Task8.Controllers;

public class TripApplicationController : ControllerBase
{
    private ApbdContext _apbdContext;

    public TripApplicationController(ApbdContext apbdContext)
    {
        _apbdContext = apbdContext;
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(ClientToTripRequest clientToTripRequest)
    {
        var timeReceived = DateTime.Now;
        if (_apbdContext.Clients.Any(arg => arg.Pesel == clientToTripRequest.Pesel))
        {
            return BadRequest("Client with a given PESEL num is already exist");
        }
        var person = await _apbdContext.Clients.FindAsync(clientToTripRequest.Pesel);
        if (_apbdContext.ClientTrips.Any(arg=> arg.IdClient == person.IdClient && arg.IdTrip == clientToTripRequest.IdTrip))
        {
            return BadRequest("Client with a given PESEL is already registered");
        }
        if (!_apbdContext.Trips.Any(arg => arg.IdTrip == clientToTripRequest.IdTrip))
        {
            return NotFound("Trip not found");
        }

        if (_apbdContext.Trips.Any(arg => arg.IdTrip == clientToTripRequest.IdTrip && arg.DateFrom > DateTime.Now))
        {
            return BadRequest("Incorrect date");
        }

        var client = new Client()
        {
            FirstName = clientToTripRequest.FirstName,
            LastName = clientToTripRequest.LastName,
            Email = clientToTripRequest.Email,
            Telephone = clientToTripRequest.Telephone,
            Pesel = clientToTripRequest.Pesel
        };
        await _apbdContext.Clients.AddAsync(client);
        await _apbdContext.SaveChangesAsync();

        var clientTrip = new ClientTrip()
        {
            IdClient = client.IdClient,
            IdTrip = clientToTripRequest.IdTrip,
            RegisteredAt = timeReceived
        };
        if (clientToTripRequest.PaymentDate != null)
        {
            clientTrip.PaymentDate = clientToTripRequest.PaymentDate;
        }

        await _apbdContext.ClientTrips.AddAsync(clientTrip);
        await _apbdContext.SaveChangesAsync();
        
        return Ok();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var check = _apbdContext.Clients.Where(arg => arg.IdClient == id && arg.ClientTrips.Any());
        if (check.Any())
        {
            return BadRequest("The trip is assigned to this client");
        }

        var person = await _apbdContext.Clients.FindAsync(id);
        _apbdContext.Clients.Remove(person);
        await _apbdContext.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        IEnumerable<Trip> trips = await _apbdContext.Trips.OrderByDescending(arg => arg.DateFrom).ToListAsync();
        return Ok(trips);
    }

    [HttpGet]
    public async Task<IActionResult> GetTripsPage()
    {
        var pageNum = 1;
        var pageSize = 10;
        if (Request.Query.ContainsKey("pageNum"))
        {
            pageNum = Convert.ToInt32(Request.Query["pageNum"]);
        }

        if (Request.Query.ContainsKey("pageSize"))
        {
            pageSize = Convert.ToInt32(Request.Query["pageSize"]);
        }

        var trips = _apbdContext.Trips.OrderByDescending(arg => arg.DateFrom);
        var countTrips = await trips.CountAsync();
        var paginatedTrips = await trips.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
        var response = new
        {
            pageNum = pageNum,
            pageSize = pageSize,
            allPages = Convert.ToInt32( countTrips / pageSize),
            Trips = paginatedTrips
        };
        
    return Ok(response);
    }
}