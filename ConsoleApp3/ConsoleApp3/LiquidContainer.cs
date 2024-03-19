namespace ConsoleApp3;

public class LiquidContainer : Container, IHazardNotifier
{
    private bool hazardous;
    public LiquidContainer(string serialNumber, double cargoMass, double height, double tareWeight, double depth, double maxPayload, bool hazardous) : base(serialNumber, cargoMass, height, tareWeight, depth, maxPayload)
    {
        SerialNumber = GenerateNum("L");
        this.hazardous = false;
    }

    public override void LoadCargo(double mass)
    {
        if (hazardous)
        {
            if (CargoMass + mass < MaxPayload * 0.5 )
            {
                CargoMass += mass;
            }
            else
            {
                throw new OverfillException("Too much weight");
            }
            NotifyHazard("Performing dangerous operation" + SerialNumber);
        }
        else
        {
            if (CargoMass + mass < MaxPayload * 0.9 )
            {
                CargoMass += mass;
            }
            else
            {
                throw new OverfillException("Too much weight");
            }
        }

    }

    public override void EmptyCargo()
    {
        CargoMass = 0;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine(message);
    }
}