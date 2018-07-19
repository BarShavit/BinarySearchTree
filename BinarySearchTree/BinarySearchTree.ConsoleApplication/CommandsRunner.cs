using System;
using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.ConsoleApplication.Commands;

namespace BinarySearchTree.ConsoleApplication
{
    public class CommandsRunner
    {
        private static readonly CommandsRunner Instance;

        public readonly Dictionary<string, Command> CommandsDictionary;

        private CommandsRunner()
        {
            CommandsDictionary = new Dictionary<string, Command>
            {
                { "help", new HelpCommand() },
                { "insert", new InsertCommand() }
            };
        }

        static CommandsRunner()
        {
            Instance = new CommandsRunner();
        }

        public static CommandsRunner GetInstance()
        {
            return Instance;
        }

        public void Run()
        {
            while (true)
            {
                var command = Console.ReadLine();
                if (command != null)
                {
                    var splitedCommand = command.Split(' ');

                    if (!CommandsDictionary.ContainsKey(splitedCommand[0]))
                    {
                        Console.WriteLine("The command doesn't exist.");
                        continue;
                    }

                    if (!CommandsDictionary[splitedCommand[0]].ValidateParams(splitedCommand.Skip(1).ToArray()))
                    {
                        Console.WriteLine("One or more of the parameters is incorrect or missing.");
                        continue;
                    }

                    Console.WriteLine();
                    CommandsDictionary[splitedCommand[0]].Execute(splitedCommand.Skip(1).ToArray());
                    Console.WriteLine();
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
