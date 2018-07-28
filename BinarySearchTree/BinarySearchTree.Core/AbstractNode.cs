using System.Collections.Generic;

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

        /// <summary>
        /// Check if the left son is a real a child
        /// </summary>
        /// <returns>true if it's a child, false if there isn't a child or it's a wire</returns>
        public bool HasALeftChild()
        {
            return LeftChild != null && LeftChild.Parent != null && LeftChild.Parent.Id == Id;
        }

        /// <summary>
        /// Check if the right son is a real a child
        /// </summary>
        /// <returns>true if it's a child, false if there isn't a child or it's a wire</returns>
        public bool HasARightChild()
        {
            return RightChild != null && RightChild.Parent != null && RightChild.Parent.Id == Id;
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

            if (currentScan.HasARightChild())
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

            if (currentScan.HasALeftChild())
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
                return HasALeftChild() ? LeftChild.Search(id) : null;

            return HasARightChild() ? RightChild.Search(id) : null;
        }

        #region Walks

        /// <summary>
        /// Preorder tree walk, moving on each node in preorder.
        /// According to the book O(n)
        /// </summary>
        /// <returns>List of the walk according to the order</returns>
        public List<AbstractNode> PreorderTreeWalk()
        {
            var list = new List<AbstractNode> {this};

            if (HasALeftChild())
                list.AddRange(LeftChild.PreorderTreeWalk());
            if (HasARightChild())
                list.AddRange(RightChild.PreorderTreeWalk());

            return list;
        }

        /// <summary>
        /// Inorder tree walk, moving on each node in inorder.
        /// According to the book O(n)
        /// </summary>
        /// <returns>List of the walk according to the order</returns>
        public List<AbstractNode> InorderTreeWalk()
        {
            var list = new List<AbstractNode>();

            if (HasALeftChild())
                list.AddRange(LeftChild.InorderTreeWalk());
            list.Add(this);
            if(HasARightChild())
                list.AddRange(RightChild.InorderTreeWalk());

            return list;
        }

        /// <summary>
        /// Postorder tree walk, moving on each node in postorder.
        /// According to the book O(n)
        /// </summary>
        /// <returns>List of the walk according to the order</returns>
        public List<AbstractNode> PostorderTreeWalk()
        {
            var list = new List<AbstractNode>();

            if (HasALeftChild())
                list.AddRange(LeftChild.PostorderTreeWalk());
            if (HasARightChild())
                list.AddRange(RightChild.PostorderTreeWalk());
            list.Add(this);

            return list;
        }

        #endregion
    }
}
