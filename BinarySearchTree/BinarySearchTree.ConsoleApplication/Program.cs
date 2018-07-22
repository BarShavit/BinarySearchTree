using System;

namespace BinarySearchTree.ConsoleApplication
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Wired Tree";
            
            Console.WriteLine("Hello, write \"help\" for options.");

            CommandsRunner.GetInstance().Run();
        }
    }
}
