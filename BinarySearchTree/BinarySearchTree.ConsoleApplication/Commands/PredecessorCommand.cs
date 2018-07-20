using System.Collections.Generic;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class PredecessorCommand<T> : Command<T> where T : AbstractNode
    {
        public PredecessorCommand(WiredTree<T> tree) : base(tree, "predecessor",
            "Get the predecessor of a student (according to his id)",
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
                WriteError("Student with id {0} doesn't exist", id);
                return;
            }

            var successor = Tree.GetPreccessor(student);

            if (successor == null)
            {
                WriteWarning("The student {0} doesn't have a predecessor.",
                    student);
                return;
            }

            WriteSuccess("The predecessor of the student {0} is {1}.",
                student, successor);
        }
    }
}
