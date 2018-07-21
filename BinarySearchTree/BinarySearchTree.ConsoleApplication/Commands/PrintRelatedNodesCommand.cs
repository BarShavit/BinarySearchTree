using System.Collections.Generic;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class PrintRelatedNodesCommand<T> : Command<T> where T : AbstractNode
    {
        public PrintRelatedNodesCommand(WiredTree<T> tree) :
            base(tree, "printRelatedNodes", "Print the sons of a node, including a symbol for wires.",
                new List<Parameter>{new Parameter("0", "id")})
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            return ReceivedIdOnlyParameter(parameters);
        }

        public override void Execute(params string[] parameters)
        {
            var id = GetIdFromFirstParameter(parameters);
            var student = Tree.Search(id);

            if (student == null)
            {
                WriteError("The student {0} doesn't exist.", id);
                return;
            }

            WriteSuccess("Parent: {0}.", student.Parent?.ToString() ?? "null");
            WriteSuccess("Left child: {0}{1}.", student.LeftChild?.ToString()
                                                ?? "null", !student.HasALeftChild() 
                                                           && student.LeftChild != null ? " (wired)" : "");
            WriteSuccess("Right child: {0}{1}.", student.RightChild?.ToString()
                                                ?? "null", !student.HasARightChild()
                                                           && student.RightChild != null ? " (wired)" : "");
        }
    }
}
