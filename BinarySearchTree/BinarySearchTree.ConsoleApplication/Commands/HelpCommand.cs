using System;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", "Get all the supported commands with a description")
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
                Console.WriteLine(commandsDictionaryValue.CommandName +
                                  " - " + commandsDictionaryValue.Description);
            }
        }
    }
}
