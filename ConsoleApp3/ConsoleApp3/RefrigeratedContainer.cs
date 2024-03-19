namespace ConsoleApp3;

public class RefrigeratedContainer : Container
{
    public RefrigeratedContainer(string serialNumber, double cargoMass, double height, double tareWeight, double depth, double maxPayload, string productType, double temperature)
        : base(serialNumber, cargoMass, height, tareWeight, depth, maxPayload)
    {
        SerialNumber = GenerateNum("R");
        ProductType = productType;
        Temperature = temperature;
    }

    public string ProductType { get; set; }
    public double Temperature { get; set; }

    public override void EmptyCargo()
    {
        CargoMass=0;
    }

    public override void LoadCargo(double mass, string asd)
    {
        if(CargoMass + mass < MaxPayload)
        {
            CargoMass += mass;
        }
        else
        {
            throw new OverfillException("Overweight");
        }
    }
}