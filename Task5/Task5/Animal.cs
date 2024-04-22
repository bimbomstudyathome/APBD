using System.ComponentModel.DataAnnotations;
namespace Task5;

public class Animal
{
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    [Required]
    [MaxLength(200)]
    public string Category { get; set; }
    [Required]
    [MaxLength(200)]
    public string Area { get; set; }
}