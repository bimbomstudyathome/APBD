using System;
using LinqTutorials.Models;

namespace LinqTutorials
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = LinqTasks.Task15();
            foreach (var res in t)
            {
                Console.WriteLine(res);
            }
            //Console.WriteLine(LinqTasks.Task13());
        }
    }
}
