using System;

namespace BinarySearchTree.ConsoleApplication
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, write \"help\" for options.");

            CommandsRunner.GetInstance().Run();
        }
    }
}
