using ConsoleApp3;

GasContainer gasContainer = new GasContainer(0, 100, 300, 30, 1000, false);
LiquidContainer liquidContainer = new LiquidContainer(100, 100, 350, 30, 1200, false);
RefrigeratedContainer refrigeratedContainer = new RefrigeratedContainer(100, 90, 230, 25, 1400, "Bananas", 13.3);
List<Container> containersExample = new List<Container>();
containersExample.Add(gasContainer);
containersExample.Add(liquidContainer);
containersExample.Add(refrigeratedContainer);
Ship ship = new Ship(20.2,20,6000, containersExample);
List<Ship> ships = new List<Ship>();
List<Container> containers = new List<Container>();
ships.Add(ship);
bool isActive = true;
int containerNum;
while (isActive)
{
    Console.WriteLine(
        "1 - create container\n2 - load cargo into container\n3 - add container to a ship\n4 - info about ship\n0 - exit");
    string str = Console.ReadLine();
    Console.WriteLine();
    switch (str)
    {
        case "1":
            create();
            break;
        case "2":
            Console.WriteLine(containers);
            Console.WriteLine("Choose order number of the container");
            containerNum = int.Parse(Console.ReadLine());
            double cargo = double.Parse(Console.ReadLine());
            containers[--containerNum].LoadCargo(cargo);
            break;
        case "3":
            Console.WriteLine(containers);
            Console.WriteLine("Choose order number of the container");
            containerNum = int.Parse(Console.ReadLine());
            Console.WriteLine("Choose order number of the ship");
            Console.WriteLine(ships);
            int shipNum = int.Parse(Console.ReadLine());
            ships[--shipNum].AddContainer(containers[--containerNum]);
            containers.RemoveAt(--containerNum);
            break;
        case "4":
            Console.WriteLine(ships);
            Console.WriteLine("Write order number: ");
            str = Console.ReadLine();
            if (ships.Count <= int.Parse(str))
            {
                ships[int.Parse(str) - 1].GetInfo();
            }
            else
            {
                Console.WriteLine("Incorrect input");
            }

            break;
        case "0":
            break;
        default:
            Console.WriteLine("Incorrect input");
            break;
    }

    void create()
    {
        Console.WriteLine("Which container?");
        Console.WriteLine("1 - refrigerated\n2 - gas\n3 - liquid");
        string str = Console.ReadLine();
        Console.WriteLine("Enter height: ");
        double height = double.Parse(Console.ReadLine());
        Console.WriteLine("Enter depth: ");
        double depth = double.Parse(Console.ReadLine());
        Console.WriteLine("Enter max payload: ");
        double maxPayload = double.Parse(Console.ReadLine());
        Console.WriteLine("Enter tare weight: ");
        double tareWeight = double.Parse(Console.ReadLine());
        bool hazard;
        string productType;
        double temp;
        switch (str)
        {
            case "1":
                Console.WriteLine("Enter product type: ");
                productType = Console.ReadLine();
                Console.WriteLine("Enter temperature: ");
                temp = double.Parse(Console.ReadLine());
                RefrigeratedContainer refrigeratedContainer =
                    new RefrigeratedContainer(0, height, tareWeight, depth, maxPayload, productType, temp);
                containers.Add(refrigeratedContainer);
                break;
            case "2":
                Console.WriteLine("Hazardous?(true/false)");
                hazard = bool.Parse(Console.ReadLine());
                GasContainer gasContainer = new GasContainer(0, height, tareWeight, depth, maxPayload, hazard);
                containers.Add(gasContainer);
                break;
            case "3":
                Console.WriteLine("Hazardous?(true/false)");
                hazard = bool.Parse(Console.ReadLine());
                LiquidContainer liquidContainer = new LiquidContainer(0, height, tareWeight, depth, maxPayload, hazard);
                containers.Add(liquidContainer);
                break;
            default:
                Console.WriteLine("Incorrect input");
                break;
        }
    }

}
