using System.ComponentModel.DataAnnotations;

namespace Task9.Models;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }

    [MaxLength(100)]
    [Required]
    public String FirstName { get; set; }

    [MaxLength(100)]
    [Required]
    public String LastName { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; }
}