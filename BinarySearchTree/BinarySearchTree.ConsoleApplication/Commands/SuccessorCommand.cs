using System.Collections.Generic;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class SuccessorCommand<T> : Command<T> where T : AbstractNode
    {
        public SuccessorCommand(WiredTree<T> tree) : base(tree, "successor", 
            "Get the successor of a student (according to his id)",
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

            var successor = Tree.GetSuccessor(student);

            if (successor == null)
            {
                WriteWarning("The student {0} doesn't have a successor.",
                    student);
                return;
            }

            WriteSuccess("The successor of the student {0} is {1}.",
                student, successor);
        }
    }
}
