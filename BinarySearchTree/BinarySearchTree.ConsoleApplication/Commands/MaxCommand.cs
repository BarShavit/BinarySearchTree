using System.Collections.Generic;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class MaxCommand<T> : Command<T> where T : AbstractNode
    {
        public MaxCommand(WiredTree<T> tree) : base(tree, "max",
            "Get the maximum of the wired tree.", new List<Parameter>())
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            return IsEmptyParameters();
        }

        public override void Execute(params string[] parameters)
        {
            var max = Tree.GetMax();

            if (max == null)
            {
                WriteWarning("The tree is empty, so there isn't a maximum.");
                return;
            }

            WriteSuccess("The student with the highest ID is {0}.", max);
        }
    }
}
