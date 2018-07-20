using System.Collections.Generic;
using System.IO;
using BinarySearchTree.Core;
using BinarySearchTree.Core.XMLReader;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class LoadStudentsFromXmlCommand<T> : Command<T> where T : AbstractNode
    {
        public LoadStudentsFromXmlCommand(WiredTree<T> tree) : 
            base(tree, "loadStudentsFromXml", "Replace all the tree data" +
                                              " with students from XML file. It will be inserted according" +
                                              " to the order in the XML file.",
                new List<Parameter>{new Parameter("0", "path of the xml file.")})
        {
        }

        public override bool ValidateParams(params string[] parameters)
        {
            return File.Exists(GetPath(parameters));
        }

        public override void Execute(params string[] parameters)
        {
            var path = GetPath(parameters);
            var serializer = new XmlSerializer<StudentsList>();
            var students = serializer.Deserialize(path);

            if (students == null)
            {
                WriteError("Couldn't read the xml file from {0}", path);
                return;
            }

            Tree.ResetTree();

            students.Students.ForEach((student) =>
                Tree.Insert(new Student(student.Id, student.Name) as T));

            WriteSuccess("The file loaded successfully.");
        }
    }
}
