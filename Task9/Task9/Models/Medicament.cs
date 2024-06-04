using System.ComponentModel.DataAnnotations;

namespace Task9.Models;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }

    [MaxLength(100)]
    [Required]
    public String Name { get; set; }

    [MaxLength(100)]
    [Required]
    public String Description { get; set; }

    [MaxLength(100)]
    [Required]
    public String Type { get; set; }

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}