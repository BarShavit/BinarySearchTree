using System.Collections.Generic;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class DeleteCommand<T> : Command<T> where T : AbstractNode
    {
        public DeleteCommand(WiredTree<T> tree) :
            base(tree, "remove",
                "Removing a student from the wired tree according to it's id.",
                new List<Parameter>{new Parameter("0", "Student's id")})
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            return ReceivedIdOnlyParameter(parameters);
        }

        public override void Execute(params string[] parameters)
        {
            var id = GetIdFromFirstParameter(parameters);
            var student = Tree.Delete(id);

            if (student == null)
            {
                WriteError("Student with id {0} doesn't exist", id);
                return;
            }

            WriteSuccess("The student {0} was removed.",
                student);
        }
    }
}
