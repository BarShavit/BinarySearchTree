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

        public readonly Dictionary<string, Command<Student>> CommandsDictionary;

        private CommandsRunner()
        {
            var tree = new WiredTree<Student>();
            CommandsDictionary = new Dictionary<string, Command<Student>>
            {
                { "help", new HelpCommand<Student>(tree) },
                { "insert", new InsertCommand<Student>(tree) },
                { "remove", new DeleteCommand<Student>(tree) },
                { "search", new SearchCommand<Student>(tree) },
                { "successor", new SuccessorCommand<Student>(tree) },
                { "predecessor", new PredecessorCommand<Student>(tree) },
                { "max", new MaxCommand<Student>(tree) },
                { "min", new MinCommand<Student>(tree) },
                { "printRelatedNodes", new PrintRelatedNodesCommand<Student>(tree) },
                { "loadStudentsFromXml", new LoadStudentsFromXmlCommand<Student>(tree) },
                { "runCommandsFromFile", new RunCommandsFromAFileCommand<Student>(tree) }
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
            if (!string.IsNullOrEmpty(command))
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
