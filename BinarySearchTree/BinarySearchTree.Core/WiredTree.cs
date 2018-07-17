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
    }
}
