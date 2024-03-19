namespace ConsoleApp3;

public abstract class Container
{
    protected Container(string serialNumber, double cargoMass, double height, double tareWeight, double depth, double maxPayload)
    {
        SerialNumber = serialNumber;
        CargoMass = cargoMass;
        Height = height;
        TareWeight = tareWeight;
        Depth = depth;
        MaxPayload = maxPayload;
    }

    public string SerialNumber { get; set; }
    public double CargoMass { get; protected set; }
    public double Height { get; }
    public double TareWeight { get; }
    public double Depth { get; }
    public double MaxPayload { get; }
    public static int Count = 1;

    public abstract void LoadCargo(double mass);
    public abstract void EmptyCargo();
    public static string GenerateNum(string type)
    {
        string number = "KON-" + type + "-" + Count++;
        return number;
    }
}