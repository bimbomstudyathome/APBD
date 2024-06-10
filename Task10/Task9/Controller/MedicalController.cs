using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task9.Context;
using Task9.DTO;
using Task9.Models;

namespace Task9.Controller;

[ApiController]
[Route("api/[controller]")]
public class MedicalController : ControllerBase
{
    private readonly MyContext _context;

    public MedicalController(MyContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> createPerscription(PerscriptionRequest perscriptionRequest)
    {
        var person = await _context.Patients.FindAsync(perscriptionRequest.PatientId);

        if (person == null)
        {
            person = new Patient
            {
                IdPatient = perscriptionRequest.PatientId,
                FirstName = perscriptionRequest.PatientFirstName,
                LastName = perscriptionRequest.PatientLastName,
                BirthDate = perscriptionRequest.PatientBirthdate
            };
            _context.Patients.Add(person);
        }

        var medicamentIds = perscriptionRequest.Medicaments.Select(m => m.MedicamentId).ToList();
        var exist = true;
        foreach (var id in medicamentIds)
        {
            var medicament = await _context.Medicaments.FindAsync(id);
            if (medicament == null)
            {
                exist = false;
            }
        }

        if (!exist)
        {
            return BadRequest("Medicament does not exist");
        }

        if (perscriptionRequest.Medicaments.Count > 10)
        {
            return BadRequest("A perscription max consist of 10 medications");
        }

        if (perscriptionRequest.DueDate < perscriptionRequest.Date)
        {
            return BadRequest("The date must be DueDate >= Date");
        }

        var prescription = new Prescription
        {
            Date = perscriptionRequest.Date,
            DueDate = perscriptionRequest.DueDate,
            Patient = person,
            Doctor = await _context.Doctors.FindAsync(perscriptionRequest.DoctorId),
            PrescriptionMedicaments = perscriptionRequest.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.MedicamentId,
                Dose = m.Dose,
                Details = m.Description
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientData(int id)
    {
        var person = await _context.Patients.FindAsync(id);

        if (person == null)
        {
            return NotFound("Patient doesnt exist");
        }

        person = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.Doctor)
            .FirstAsync(p => p.IdPatient == id);

        var perscriptions = person.Prescriptions.OrderBy(p => p.DueDate).ToList();

        var result = new
        {
            person.IdPatient,
            person.FirstName,
            person.LastName,
            person.BirthDate,
            Prescriptions = perscriptions.Select(pr => new
            {
                pr.IdPrescription,
                pr.Date,
                pr.DueDate,
                Doctor = new
                {
                    pr.Doctor.IdDoctor,
                    pr.Doctor.FirstName,
                    pr.Doctor.LastName,
                    pr.Doctor.Email
                },
                Medicaments = pr.PrescriptionMedicaments.Select(pm => new
                {
                    pm.Medicament.IdMedicament,
                    pm.Medicament.Name,
                    pm.Medicament.Description,
                    pm.Medicament.Type,
                    pm.Dose,
                    pm.Details
                }).ToList()
            }).ToList()
        };

        return Ok(result);
    }
}