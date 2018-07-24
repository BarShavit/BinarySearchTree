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
            var walk = Tree.PreorderTreeWalk();
            if (walk == null)
            {
                WriteWarning("The tree is empty.");
                return;
            }
            PrintChainList(walk.Select(
                node => node as object).ToList());
        }
    }
}
