namespace ConsoleApp3;

public class LiquidContainer : Container, IHazardNotifier
{
    private bool _hazardous;
    public LiquidContainer(double cargoMass, double height, double tareWeight, double depth, double maxPayload, bool hazardous) : base( cargoMass, height, tareWeight, depth, maxPayload)
    {
        SerialNumber = GenerateNum("L");
        _hazardous = false;
    }

    public override void LoadCargo(double mass)
    {
        if (_hazardous)
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

    public override string ToString()
    {
        return CargoMass + " " + SerialNumber + " " + TareWeight + " " + Depth + " " + Height + _hazardous ;
    }
}