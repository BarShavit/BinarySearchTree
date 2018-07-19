using System;
using System.Linq;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class InsertCommand : Command
    {
        public InsertCommand() : 
            base("insert", "Insert a new student to the wired tree." +
                           " First parameter should be student's id " +
                           "(9 numbers) and rest of the parameters will be the name.")
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            int id;
            return parameters[0].Length == 9 && 
                   int.TryParse(parameters[0], out id) && parameters.Length > 1;
        }

        public override void Execute(params string[] parameters)
        {
            var id = int.Parse(parameters[0]);
            var name = string.Join(" ", parameters.Skip(1));

            WiredTree<Student>.Instance.Insert(new Student(id, name));

            Console.WriteLine(
                string.Format("The student {0} - {1} was added to the wired tree.", id, name));
        }
    }
}
