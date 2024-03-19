namespace ConsoleApp3;

public class GasContainer : Container, IHazardNotifier
{
    private bool hazardous;
    public GasContainer(string serialNumber, double cargoMass, double height, double tareWeight, double depth, double maxPayload, bool hazardous) : base(serialNumber, cargoMass, height, tareWeight, depth, maxPayload)
    {
        SerialNumber = GenerateNum("G");
        this.hazardous = hazardous;
    }  

    public override void LoadCargo(double mass)
    {
        if (hazardous)
        {
            Console.WriteLine("Performing dangerous operation" + SerialNumber);
        }

        if (CargoMass + mass < MaxPayload)
        {
            CargoMass += mass;
        }
    }

    public override void EmptyCargo()
    {
        CargoMass *= 0.05;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine(message);
    }
}