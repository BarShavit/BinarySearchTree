using System;
using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class HelpCommand<T> : Command<T> where T : AbstractNode
    {
        public HelpCommand(WiredTree<T> tree) : base(tree, "help", "Get all the supported commands with a description",
            new List<Parameter>())
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            return true;
        }

        public override void Execute(params string[] parameters)
        {
            Console.WriteLine("The supported commands are:");
            foreach (var commandsDictionaryValue in CommandsRunner.GetInstance().CommandsDictionary.Values)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(commandsDictionaryValue.CommandName);
                Console.ResetColor();
                Console.WriteLine(commandsDictionaryValue.Description);

                if (commandsDictionaryValue.Parameters.Any())
                {
                    Console.WriteLine("Parameters:");
                    commandsDictionaryValue.Parameters.ForEach(parameter =>
                    {
                        Console.WriteLine("- {0} {1}", parameter.Index, parameter.Name);
                    });
                }
            }
        }
    }
}
