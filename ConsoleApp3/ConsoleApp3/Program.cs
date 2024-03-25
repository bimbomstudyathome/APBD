using System.Collections;
using ConsoleApp3;

GasContainer gasContainer = new GasContainer(0, 100, 300, 30, 1000, false);
LiquidContainer liquidContainer = new LiquidContainer(100, 100, 350, 30, 1200, false);
RefrigeratedContainer refrigeratedContainer = new RefrigeratedContainer(100, 90, 230, 25, 1400, "Bananas", 13.3);
List<Container> containersExample = new List<Container>();
containersExample.Add(gasContainer);
containersExample.Add(liquidContainer);
containersExample.Add(refrigeratedContainer);
Ship ship = new Ship(20.2,20,60000);
ship.AddListContainers(containersExample);
List<Ship> ships = new List<Ship>();
List<Container> containers = new List<Container>();
ships.Add(ship);
bool isActive = true;
int containerNum;
while (isActive)
{
    Console.WriteLine(
        "1 - create container\n2 - load cargo into container\n3 - add container to a ship\n4 - get general info\n5 - remove container" +
        "\n6 - move container to another ship\n7 - unload container\n8 - replace with a different container\n9 - add list of containers\n10 - add new ship\n0 - exit");
    string str = Console.ReadLine();
    Console.WriteLine();
    switch (str)
    {
        case "1":
            create();
            break;
        case "2":
            foreach (var arg in containers)
            {
                Console.WriteLine($"Serial number: {arg.SerialNumber}, cargo mass: {arg.CargoMass}");
            }
            Console.WriteLine("Choose order number of the container");
            containerNum = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter mass of the cargo");
            double cargo = double.Parse(Console.ReadLine());
            try
            {
                containers[--containerNum].LoadCargo(cargo);
            }
            catch (OverfillException e)
            {
                Console.WriteLine(e);
            }
            break;
        case "3":
            foreach (var arg in containers)
            {
                Console.WriteLine($"Serial number: {arg.SerialNumber}, cargo mass: {arg.CargoMass}");
            }
            Console.WriteLine("Choose order number of the container");
            containerNum = int.Parse(Console.ReadLine());
            foreach (var arg in ships)
            {
                Console.WriteLine($"Serial number: {arg.Id}, max amount of containers: {arg.MaxContainerAmount}");
            }
            Console.WriteLine("Choose order number of the ship");
            int shipNum = int.Parse(Console.ReadLine());
            ships[--shipNum].AddContainer(containers[--containerNum]);
            containers.RemoveAt(containerNum);
            break;
        case "4":
            foreach (var arg in ships)
            {
                Console.WriteLine($"Serial number: {arg.Id}, max amount of containers: {arg.MaxContainerAmount}");
            }
            Console.WriteLine("Write order number of the ship above: ");
            str = Console.ReadLine();
            if (ships.Count >= int.Parse(str))
            {
                ships[int.Parse(str) - 1].GetInfo();
            }
            else
            {
                Console.WriteLine("Incorrect input");
            }
            Console.WriteLine("Do you want to see information about specific container?(1 - yes, 2 - no)");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                Console.WriteLine("Choose container: ");
                foreach (var arg in ships[int.Parse(str) - 1].Containers)
                {
                    Console.WriteLine($"Serial number: {arg.SerialNumber}, cargo mass: {arg.CargoMass}");
                }
                containerNum = int.Parse(Console.ReadLine());
                Console.WriteLine(ships[int.Parse(str) - 1].Containers[containerNum - 1]);
            }
            break;
        case "5":
            Console.WriteLine("Select ship: ");
            foreach (var arg in ships)
            {
                Console.WriteLine($"Serial number: {arg.Id}, max amount of containers: {arg.MaxContainerAmount}");
            }
            str = Console.ReadLine();
            Console.WriteLine("Choose container: ");
            foreach (var arg in ships[int.Parse(str) - 1].Containers)
            {
                Console.WriteLine($"Serial number: {arg.SerialNumber}, cargo mass: {arg.CargoMass}");
            }
            Console.WriteLine("Choose container number");
            containerNum = int.Parse(Console.ReadLine());
            containers.Add(ships[int.Parse(str) - 1 ].Containers[int.Parse(str) - 1]);
            ships[int.Parse(str) - 1].RemoveContainer(containerNum);
            break;
        case "6":
            Console.WriteLine("Select ship: ");
            foreach (var arg in ships)
            {
                Console.WriteLine($"Serial number: {arg.Id}, max amount of containers: {arg.MaxContainerAmount}");
            }
            str = Console.ReadLine();
            foreach (var arg in ships[int.Parse(str) - 1].Containers)
            {
                Console.WriteLine($"Serial number: {arg.SerialNumber}, cargo mass: {arg.CargoMass}");
            }
            Console.WriteLine("Choose container number");
            containerNum = int.Parse(Console.ReadLine());
            Console.WriteLine("Select second ship: ");
            foreach (var arg in ships)
            {
                Console.WriteLine($"Serial number: {arg.Id}, max amount of containers: {arg.MaxContainerAmount}");
            }
            int secondShip = int.Parse(Console.ReadLine());
            ships[secondShip - 1].AddContainer(ships[int.Parse(str) - 1].Containers[containerNum - 1]);
            ships[int.Parse(str) - 1].RemoveContainer(containerNum );
            break;
        case "7":
            Console.WriteLine("Select ship: ");
            foreach (var arg in ships)
            {
                Console.WriteLine($"Serial number: {arg.Id}, max amount of containers: {arg.MaxContainerAmount}");
            }
            str = Console.ReadLine();
            Console.WriteLine("Choose container: ");
            foreach (var arg in ships[int.Parse(str) - 1].Containers)
            {
                Console.WriteLine($"Serial number: {arg.SerialNumber}, cargo mass: {arg.CargoMass}");
            }
            Console.WriteLine("Choose container number");
            containerNum = int.Parse(Console.ReadLine());
            ships[int.Parse(str) - 1].Containers[containerNum - 1].EmptyCargo();
            break;
        case "8":
            Console.WriteLine("Select ship: ");
            foreach (var arg in ships)
            {
                Console.WriteLine($"Serial number: {arg.Id}, max amount of containers: {arg.MaxContainerAmount}");
            }
            str = Console.ReadLine();
            foreach (var arg in ships[int.Parse(str) - 1].Containers)
            {
                Console.WriteLine($"Serial number: {arg.SerialNumber}, cargo mass: {arg.CargoMass}");
            }
            Console.WriteLine("Choose container number");
            containerNum = int.Parse(Console.ReadLine());
            Container oldContainer = ships[int.Parse(str) - 1].Containers[containerNum - 1];
            ships[int.Parse(str) - 1].RemoveContainer(containerNum);
            foreach (var arg in containers)
            {
                Console.WriteLine($"Serial number: {arg.SerialNumber}, cargo mass: {arg.CargoMass}");
            }
            Console.WriteLine("Choose a container to replacd with");
            containerNum = int.Parse(Console.ReadLine());
            ships[int.Parse(str) - 1].AddContainer(containers[containerNum - 1]);
            containers.RemoveAt(containerNum - 1);
            containers.Add(oldContainer);
            break;
        case "9":
            List<Container> tempContainers = new List<Container>();
            bool isEnough = true;
            while (isEnough)
            {
                foreach (var arg in containers)
                {
                    Console.WriteLine($"Serial number: {arg.SerialNumber}, cargo mass: {arg.CargoMass}");
                }
                Console.WriteLine("Choose order number of the container");
                containerNum = int.Parse(Console.ReadLine());
                tempContainers.Add(containers[containerNum - 1]);
                containers.RemoveAt(containerNum - 1);
                Console.WriteLine("That's all?(yes/no)");
                str = Console.ReadLine();
                if (str == "yes")
                {
                    isEnough = false;
                }
            }
            foreach (var arg in ships)
            {
                Console.WriteLine($"Serial number: {arg.Id}, max amount of containers: {arg.MaxContainerAmount}");
            }
            Console.WriteLine("Choose order number of the ship");
            str = Console.ReadLine();
            ships[int.Parse(str) - 1].AddListContainers(tempContainers);
            tempContainers = null;
            break;
        case "10":
            Console.WriteLine("Enter max speed");
            double maxSpeed = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter max amount of containers");
            int maxAmount = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter max weight of teh ship");
            double maxWeight = double.Parse(Console.ReadLine());
            Ship temp = new Ship(maxSpeed, maxAmount, maxWeight);
            ships.Add(temp);
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
