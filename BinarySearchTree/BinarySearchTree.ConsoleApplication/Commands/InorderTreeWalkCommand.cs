using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class InorderTreeWalkCommand<T> : Command<T> where T : AbstractNode
    {
        public InorderTreeWalkCommand(WiredTree<T> tree) : base(tree,
            "inorderWalk", "Print the inorder walk of the tree.", new List<Parameter>())
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            return true;
        }

        public override void Execute(params string[] parameters)
        {
            PrintChainList(Tree.InorderTreeWalk().Select(
                node => node as object).ToList());
        }
    }
}
