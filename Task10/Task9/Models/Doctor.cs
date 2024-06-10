using System.ComponentModel.DataAnnotations;

namespace Task9.Models;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }

    [MaxLength(100)]
    [Required]
    public String FirstName { get; set; }

    [MaxLength(100)]
    [Required]
    public String LastName { get; set; }

    [MaxLength(100)]
    [Required]
    public String Email { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; }
}