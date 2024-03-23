namespace ConsoleApp3;

public class GasContainer : Container, IHazardNotifier
{
    private bool _hazardous;
    public GasContainer( double cargoMass, double height, double tareWeight, double depth, double maxPayload, bool hazardous) : base( cargoMass, height, tareWeight, depth, maxPayload)
    {
        SerialNumber = GenerateNum("G");
        _hazardous = hazardous;
    }  

    public override void LoadCargo(double mass)
    {
        if (_hazardous)
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

    public override string ToString()
    {
        return base.ToString();
    }
}