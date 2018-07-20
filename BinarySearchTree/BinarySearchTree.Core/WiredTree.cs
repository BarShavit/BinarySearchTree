namespace BinarySearchTree.Core
{
    public class WiredTree<T> where T : AbstractNode
    {
        public T Root;
        public T Median;

        private int _nodesSmallerThanTheMedian;
        private int _nodesBiggerThanTheMedian;

        public WiredTree()
        {
            ResetTree();
        }

        /// <summary>
        /// Delete the root and median
        /// reset counters
        /// start from scratch
        /// </summary>
        public void ResetTree()
        {
            Root = null;
            Median = null;
            _nodesSmallerThanTheMedian = 0;
            _nodesBiggerThanTheMedian = 0;
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

            UpdateMedianAfterInsert(node);
        }

        /// <summary>
        /// Delete the node from the tree.
        /// According to the book page 221 it's O(h)
        /// </summary>
        /// <param name="node">Node for delete</param>
        public void Delete(T node)
        {
            if (node == null) return;
            var deletedTheOldMedian = false;

            UpdateWiresRelatedToRemovedNode(node);

            // If we delete the current median, the counters are already updated
            // so we can update it now.
            if (node.Id == Median.Id)
            {
                UpdateMedianAfterCountersUpdated();
                deletedTheOldMedian = true;
            }

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
                nextNode.Parent = nodeForRemove.Parent;

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

            // If we deleted another node(not the median), update the median
            // if needed
            if (!deletedTheOldMedian)
                UpdateMedianAfterRemoveNotMedian(node);
        }

        /// <summary>
        /// Delete a student from the tree according to the id
        /// using search to find the student at first.
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>The student which deleted</returns>
        public T Delete(int id)
        {
            var student = Search(id);
            Delete(student);
            return student;
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
            return Root != null ? Root.Search(id) as T : null;
        }

        #region Median helpers

        /// <summary>
        /// After we insert a new node, we update the median to the new
        /// upper one
        /// </summary>
        /// <param name="node">The new node</param>
        private void UpdateMedianAfterInsert(T node)
        {
            // if the median is null, this node is the only node of the tree
            // so it will be the median
            if(Median == null)
            {
                Median = node;
                return;
            }

            // Update the counters of nodes bigger than the median and smaller
            if (Median.Id < node.Id)
                _nodesBiggerThanTheMedian++;
            else
                _nodesSmallerThanTheMedian++;

            UpdateMedianAfterCountersUpdated();
        }

        /// <summary>
        /// Update the median after remove if the removed node
        /// isn't the current median
        /// </summary>
        /// <param name="node">The deleted node</param>
        private void UpdateMedianAfterRemoveNotMedian(T node)
        {
            // Update the counters of nodes bigger than the median and smaller
            if (Median.Id < node.Id)
                _nodesBiggerThanTheMedian--;
            else
                _nodesSmallerThanTheMedian--;

            UpdateMedianAfterCountersUpdated();
        }

        /// <summary>
        /// Update the median after the counters updated.
        /// We need to replace the median if the bigger 
        /// counter is bigger the the small counter,
        /// or if the smaller counter is smaller than the bigger counter + 1
        /// because we choose the upper median
        /// </summary>
        private void UpdateMedianAfterCountersUpdated()
        {
            if (_nodesBiggerThanTheMedian > _nodesSmallerThanTheMedian)
            {
                Median = Median.GetSuccessor() as T;
                _nodesBiggerThanTheMedian--;
                _nodesSmallerThanTheMedian++;
                return;
            }

            if (_nodesSmallerThanTheMedian > _nodesBiggerThanTheMedian + 1)
            {
                Median = Median.GetPredecessor() as T;
                _nodesBiggerThanTheMedian++;
                _nodesSmallerThanTheMedian--;
            }
        }

        #endregion

        #region Delete helpers
        

        /// <summary>
        /// When we remove a node, we check if his successor / predecessor
        /// has a wire to him. If yes, we update the wire to the node's predecessor
        /// or successor
        /// </summary>
        /// <param name="node">The removed node</param>
        private void UpdateWiresRelatedToRemovedNode(T node)
        {
            var successor = node.GetSuccessor();
            if (successor != null && !successor.HasALeftChild() && successor.LeftChild == node)
            {
                successor.LeftChild = node.GetPredecessor();
            }

            var predecessor = node.GetPredecessor();
            if (predecessor != null && !predecessor.HasARightChild() && predecessor.RightChild == node)
            {
                predecessor.LeftChild = node.GetSuccessor();
            }
        }

        #endregion
    }
}
