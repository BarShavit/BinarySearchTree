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
        /// Clone the received node's data to this object
        /// </summary>
        /// <param name="node">The received node</param>
        public virtual void Clone(object node)
        {
            var castedNode = node as AbstractNode;
            if (castedNode != null)
                Id = castedNode.Id;
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

        /// <summary>
        /// Get the successor of this node,
        /// according to the book it's O(h)
        /// </summary>
        /// <returns>The successor, if doesn't exist returns null</returns>
        public AbstractNode GetSuccessor()
        {
            var currentScan = this;

            if (currentScan.RightChild != null)
                return currentScan.RightChild.GetMinimum();

            var successor = currentScan.Parent;

            while(successor != null && currentScan == successor.RightChild)
            {
                currentScan = successor;
                successor = successor.Parent;
            }

            return successor;
        }

        /// <summary>
        /// Get the predecessor of this node,
        /// according to the book it's O(h)
        /// </summary>
        /// <returns>The successor, if doesn't exist returns null</returns>
        public AbstractNode GetPredecessor()
        {
            var currentScan = this;

            if (currentScan.LeftChild != null)
                return currentScan.LeftChild.GetMax();

            var predeccessor = currentScan.Parent;

            while (predeccessor != null && currentScan == predeccessor.LeftChild)
            {
                currentScan = predeccessor;
                predeccessor = predeccessor.Parent;
            }

            return predeccessor;
        }

        /// <summary>
        /// Search the node with the id within the subtree of this node
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>The node, if doesn't exist - returns null</returns>
        public AbstractNode Search(int id)
        {
            if (Id == id)
                return this;

            if (id < Id)
                return HasALeftChild() ? LeftChild.LeftChild.Search(id) : null;

            return HasARightChild() ? RightChild.RightChild.Search(id) : null;
        }
    }
}
