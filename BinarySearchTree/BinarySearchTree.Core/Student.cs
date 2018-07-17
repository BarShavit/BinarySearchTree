namespace BinarySearchTree.Core
{
    /// <summary>
    /// A student, will be used as the tree's node object
    /// </summary>
    public class Student : AbstractNode
    {
        public string Name;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">Student's id</param>
        /// <param name="name">Student's name</param>
        public Student(int id, string name) : base(id)
        {
            Name = name;
        }
    }
}
