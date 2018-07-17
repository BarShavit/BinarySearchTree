namespace BinarySearchTree.Core
{
    public abstract class AbstractNode
    {
        public int Id;


        public AbstractNode Parent;
        public AbstractNode RightChild;
        public AbstractNode LeftChild;

        protected AbstractNode(int id)
        {
            Id = id;
            Parent = null;
        }

        /// <summary>
        /// The node is a leaf only if he doesn't have sons
        /// or if the given son's parent isn't this node
        /// </summary>
        /// <returns>True if leaf, else false</returns>
        public bool IsLeaf()
        {
            return !HasALeftChild() && !HasARightChild();
        }

        public bool HasALeftChild()
        {
            return LeftChild == null || LeftChild.Parent.Id != Id;
        }

        public bool HasARightChild()
        {
            return RightChild == null || RightChild.Parent.Id != Id;
        }

        /// <summary>
        /// Get the minimum of the sub-tree with this node as root
        /// O(h) - h is the height of the sub tree
        /// </summary>
        public AbstractNode GetMinimum()
        {
            var temp = this;
            while (temp.HasALeftChild())
                temp = temp.LeftChild;
            return temp;
        }

        /// <summary>
        /// Get the maximum of the sub-tree with this node as root
        /// O(h) - h is the height of the sub tree
        /// </summary>
        public AbstractNode GetMax()
        {
            var temp = this;
            while (temp.HasARightChild())
                temp = temp.RightChild;
            return temp;
        }
    }
}
