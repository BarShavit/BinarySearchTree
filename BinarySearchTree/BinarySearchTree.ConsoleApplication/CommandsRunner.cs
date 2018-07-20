using System;
using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.ConsoleApplication.Commands;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication
{
    public class CommandsRunner
    {
        private static readonly CommandsRunner Instance;

        private readonly WiredTree<Student> _tree;
        public readonly Dictionary<string, Command<Student>> CommandsDictionary;

        private CommandsRunner()
        {
            _tree = new WiredTree<Student>();
            CommandsDictionary = new Dictionary<string, Command<Student>>
            {
                { "help", new HelpCommand<Student>(_tree) },
                { "insert", new InsertCommand<Student>(_tree) },
                { "remove", new DeleteCommand<Student>(_tree) },
                { "search", new SearchCommand<Student>(_tree) },
                { "successor", new SuccessorCommand<Student>(_tree) },
                { "predecessor", new PredecessorCommand<Student>(_tree) },
                { "max", new MaxCommand<Student>(_tree) },
                { "min", new MinCommand<Student>(_tree) }
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
                Console.Write("> ");
                var command = Console.ReadLine();
                HandleCommand(command);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public void HandleCommand(string command)
        {
            if (command != null)
            {
                var splitedCommand = command.Split(' ');

                if (!CommandsDictionary.ContainsKey(splitedCommand[0]))
                {
                    Console.WriteLine("The command doesn't exist.");
                    return;
                }

                if (!CommandsDictionary[splitedCommand[0]].ValidateParams(splitedCommand.Skip(1).ToArray()))
                {
                    Console.WriteLine("One or more of the parameters is incorrect or missing.");
                    return;
                }

                Console.WriteLine();
                CommandsDictionary[splitedCommand[0]].Execute(splitedCommand.Skip(1).ToArray());
                Console.WriteLine();
            }
        }
    }
}
