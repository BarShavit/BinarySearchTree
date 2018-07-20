using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class PostorderTreeWalkCommand<T> : Command<T> where T : AbstractNode
    {
        public PostorderTreeWalkCommand(WiredTree<T> tree) : base(tree, 
            "postorderWalk", "Print the postorder walk of the tree.",
            new List<Parameter>())
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            return true;
        }

        public override void Execute(params string[] parameters)
        {
            PrintChainList(Tree.PostorderTreeWalk().Select(
                node => node as object).ToList());
        }
    }
}
