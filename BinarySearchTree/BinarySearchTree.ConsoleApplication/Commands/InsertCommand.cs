using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class InsertCommand<T> : Command<T> where T : AbstractNode
    {
        public InsertCommand(WiredTree<T> tree) : 
            base(tree, "insert", "Insert a new student to the wired tree.",
                new List<Parameter> { new Parameter("0", "Student's id"),
                    new Parameter("1 - 50", "Student's full name") })
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            int id;
            return int.TryParse(parameters[0], out id) && parameters.Length > 1;
        }

        public override void Execute(params string[] parameters)
        {
            var id = GetIdFromFirstParameter(parameters);
            var name = string.Join(" ", parameters.Skip(1));

            Tree.Insert(new Student(id, name) as T);

            WriteSuccess("The student \"{0} - {1}\" was added to the wired tree.", id, name);
        }
    }
}
