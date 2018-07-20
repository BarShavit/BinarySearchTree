using System.Collections.Generic;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class SearchCommand<T> : Command<T> where T : AbstractNode
    {
        public SearchCommand(WiredTree<T> tree) : base(tree, "search",
            "Searching a student according to his id",
            new List<Parameter> { new Parameter("0", "Student's id") })
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
                WriteError("The student {0} doesn't exists.", id);
                return;
            }

            WriteSuccess("Found student {0}", student);
        }
    }
}
