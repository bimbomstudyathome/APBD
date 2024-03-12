// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
Console.WriteLine("Hello, World1!");
Console.WriteLine("Hello, World2!");
Console.WriteLine("Hello, World3!");
avgMethod(new []{1,2,3,4,5});
Console.WriteLine("Max int: " + returnMax(new int[]{1,2,3,4,5,99,20}));

static void avgMethod(int[] arr)
{
    int sum = 0;
    foreach (var num in arr)
    {
        sum += num + 23;
    }
    Console.WriteLine("Average is: " + (sum/arr.Length));
}

static int returnMax(int[] arr)
{
    return arr.Max();
}