using System.ComponentModel.DataAnnotations;

namespace Task9.Models;

public class ApplicationUser
{
    [Key]
    public int IdUser { get; set; }
    [Required]
    public String Username { get; set; }
    [Required]
    public String Password { get; set; }
    [Required]
    public String Salt { get; set; }
    [Required]
    public String RefreshToken { get; set; }
    [Required]
    public DateTime? RefreshTokenExp { get; set; }
}