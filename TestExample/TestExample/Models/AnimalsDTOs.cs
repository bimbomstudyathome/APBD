namespace TestExample.Models;

public class AnimalsDTOs
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime AdmissionDate { get; set; }
    public Owner Owner{ get; set; }
    public List<Procedure> Procedures { get; set; }
}