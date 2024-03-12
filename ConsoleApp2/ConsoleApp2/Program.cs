// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
Console.WriteLine("Hello, World1!");
Console.WriteLine("Hello, World2!");
Console.WriteLine("Hello, World3!");
avgMethod(new []{1,2,3,4,5});

static void avgMethod(int[] arr)
{
    int sum = 0;
    foreach (var num in arr)
    {
        sum += num;
    }
    Console.WriteLine("Average is: " + (sum/arr.Length));
}