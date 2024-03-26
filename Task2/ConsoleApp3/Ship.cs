namespace ConsoleApp3;

public class Ship
{
    public int Id { get; set; }
    public static int Count = 1;
    public double MaxSpeed{ get; set; }
    public int MaxContainerAmount{ get; set; }
    public double MaxWeight{ get; set; }    
    public List<Container> Containers { get; set; }

    public Ship(double maxSpeed, int maxContainerAmount, double maxWeight)
    {
        Id = Count++;
        MaxSpeed = maxSpeed;
        MaxContainerAmount = maxContainerAmount;
        MaxWeight = maxWeight;
        Containers = new List<Container>();
    }

    public void AddContainer(Container container)
    {
        if (Containers.Count >= MaxContainerAmount)
        {
            Console.WriteLine("Too many containers");
            return;
        }

        double total = Containers.Sum(arg => arg.CargoMass + arg.TareWeight);
        if (total + container.TareWeight + container.CargoMass < MaxWeight)
        {
            Containers.Add(container);
        }
        else
        {
            Console.WriteLine("Too much weight");
        }
    }

    public void AddListContainers(List<Container> containers)
    {
        foreach (var container in containers)
        {
            AddContainer(container);
        }
    }

    public void RemoveContainer(int Num)
    {
        if (Containers.Count() >= Num)
        {
            Containers.RemoveAt(Num - 1);
        }
        else
        {
            Console.WriteLine("Incorrect order number");
        }
    }

    public void GetInfo()
    {
        Console.WriteLine($"Ship id: {Id}");
        Console.WriteLine($"Max speed is: {MaxSpeed}");
        Console.WriteLine($"Max Weight is: {MaxWeight}");
        Console.WriteLine($"Max container amount is: {MaxContainerAmount}");
        Console.WriteLine("All containers on the ship: ");
        foreach (var container in Containers)
        {
            Console.WriteLine($"Container number: {container.SerialNumber}, current cargo weight: {container.CargoMass}");
        }
        
    }
}