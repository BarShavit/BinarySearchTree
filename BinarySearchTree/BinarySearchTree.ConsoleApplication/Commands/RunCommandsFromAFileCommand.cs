using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class RunCommandsFromAFileCommand<T> : Command<T> where T : AbstractNode
    {
        public RunCommandsFromAFileCommand(WiredTree<T> tree) :
            base(tree, "runCommandsFromFile", "Read commands from a file and execute them" +
                                              " according to their order.",
                new List<Parameter> {new Parameter("0", "path")})
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            return File.Exists(GetPath(parameters));
        }

        public override void Execute(params string[] parameters)
        {
            // Read all the file before we execute the commands,
            // the handling may take along time and we don't want
            // to keep the stream active for too long
            var commands = File.ReadAllLines(GetPath(parameters)).ToList();
            
            commands.ForEach(command =>
            {
                Console.WriteLine("Executing \"{0}\"", command);
                CommandsRunner.GetInstance().HandleCommand(command);
            });
        }
    }
}
