namespace BinarySearchTree.Core
{
    public class WiredTree<T> where T : AbstractNode
    {
        public T Root;

        public WiredTree()
        {
            Root = null;
        }

        /// <summary>
        /// Insert the node to the tree according to the algorithm in
        /// page 220. According to the book it runs in O(h)
        /// </summary>
        /// <param name="node"></param>
        public void Insert(T node)
        {
            if (node == null) return;
            
            AbstractNode newNodeParent = null;
            var searchParentNode = Root as AbstractNode;

            while (searchParentNode != null)
            {
                newNodeParent = searchParentNode;
                if (node.Id < searchParentNode.Id && searchParentNode.HasALeftChild())
                    searchParentNode = searchParentNode.LeftChild;
                else if (searchParentNode.HasARightChild())
                    searchParentNode = searchParentNode.RightChild;
                else
                    searchParentNode = null;
            }

            node.Parent = newNodeParent;

            // If newNodeParent is null, the tree is empty and
            // it will be the new root
            if (newNodeParent == null)
                Root = node;
            else
            {
                if (node.Id < newNodeParent.Id)
                    newNodeParent.LeftChild = node;
                else
                    newNodeParent.RightChild = node;
            }

            // Wire the new node (which is a leaf) to successor and predecessor
            node.RightChild = node.GetSuccessor();
            node.LeftChild = node.GetPredecessor();
        }

        /// <summary>
        /// Delete the node from the tree.
        /// According to the book page 221 it's O(h)
        /// </summary>
        /// <param name="node">Node for delete</param>
        public void Delete(T node)
        {
            AbstractNode nodeForRemove, nextNode = null;

            if (!node.HasALeftChild() || !node.HasARightChild())
                nodeForRemove = node;
            else
                nodeForRemove = node.GetSuccessor();

            if (nodeForRemove.HasALeftChild())
                nextNode = nodeForRemove.LeftChild;
            else if (nodeForRemove.HasARightChild())
                nextNode = nodeForRemove.RightChild;

            if (nextNode != null)
                nextNode.Parent = nodeForRemove;

            if (nodeForRemove.Parent == null)
                Root = nextNode as T;
            else
            {
                if (nodeForRemove.Id == nodeForRemove.Parent.LeftChild.Id)
                {
                    nodeForRemove.Parent.LeftChild = nextNode;

                    // If after this change, the parent gets a "null child"
                    // instead of the delete node, wire it to predecessor
                    if (nodeForRemove.Parent.LeftChild == null)
                        nodeForRemove.Parent.LeftChild = nodeForRemove.Parent.GetPredecessor();
                }
                else
                {
                    nodeForRemove.Parent.RightChild = nextNode;

                    // If after this change, the parent gets a "null child"
                    // instead of the delete node, wire it to successor
                    if (nodeForRemove.Parent.RightChild == null)
                        nodeForRemove.Parent.RightChild = nodeForRemove.Parent.GetSuccessor();
                }
            }

            if (nodeForRemove.Id != node.Id)
            {
                node.Clone(nodeForRemove);
            }
        }

        /// <summary>
        /// Get the minimum of the tree.
        /// Using the method in the root so it's O(h)
        /// </summary>
        /// <returns>minimum</returns>
        public T GetMinimum()
        {
            if (Root == null) return null;

            return Root.GetMinimum() as T;
        }

        /// <summary>
        /// Get the max of the tree.
        /// Using the method in the root so it's O(h)
        /// </summary>
        /// <returns>minimum</returns>
        public T GetMax()
        {
            if (Root == null) return null;

            return Root.GetMax() as T;
        }

        /// <summary>
        /// Get the successor of the node
        /// </summary>
        /// <param name="node">The node</param>
        /// <returns>Successor, null if doesn't exist</returns>
        public T GetSuccessor(T node)
        {
            return node.GetSuccessor() as T;
        }

        /// <summary>
        /// Get the preccessor.
        /// </summary>
        public T GetPreccessor(T node)
        {
            return node.GetPredecessor() as T;
        }

        /// <summary>
        /// Search the id by the node's method
        /// </summary>
        public T Search(T node, int id)
        {
            return node.Search(id) as T;
        }

        /// <summary>
        /// Search the id by the node's method
        /// </summary>
        public T Search(int id)
        {
            return Root.Search(id) as T;
        }
    }
}
