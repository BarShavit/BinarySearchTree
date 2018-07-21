using System.Collections.Generic;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class MedianCommand<T> : Command<T> where T : AbstractNode
    {
        public MedianCommand(WiredTree<T> tree) :
            base(tree, "median", "Get the median of the tree.", new List<Parameter>())
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            return true;
        }

        public override void Execute(params string[] parameters)
        {
            if (Tree.Median == null)
            {
                WriteSuccess("The tree is empty, there isn't a median right now.");
                return;
            }

            WriteSuccess("The median is {0}.", Tree.Median.ToString());
        }
    }
}
