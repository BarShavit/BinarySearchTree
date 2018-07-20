using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class PreorderTreeWalkCommand<T> : Command<T> where T : AbstractNode
    {
        public PreorderTreeWalkCommand(WiredTree<T> tree) : 
            base(tree, "preorderWalk", "Print the preorder walk of the tree.", 
                new List<Parameter>())
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            return true;
        }

        public override void Execute(params string[] parameters)
        {
            PrintChainList(Tree.PreorderTreeWalk().Select(
                node => node as object).ToList());
        }
    }
}
