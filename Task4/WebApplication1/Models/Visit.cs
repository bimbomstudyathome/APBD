namespace WebApplication1.Models;

public class Visit
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DateTime { get; set; }
    public Animal Animal { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }

}