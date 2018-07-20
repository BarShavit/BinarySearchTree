using System.Collections.Generic;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class MinCommand<T> : Command<T> where T : AbstractNode
    {
        public MinCommand(WiredTree<T> tree) : base(tree, "min", 
            "Get the minimum of the wired tree.", new List<Parameter>())
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            return IsEmptyParameters();
        }

        public override void Execute(params string[] parameters)
        {
            var min = Tree.GetMinimum();

            if (min == null)
            {
                WriteWarning("The tree is empty, so there isn't a minimum.");
                return;
            }

            WriteSuccess("The student with the lowest ID is {0}.", min);
        }
    }
}
