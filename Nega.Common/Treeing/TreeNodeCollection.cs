using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    /// <summary>
    /// 定义了树节点集合的操作。
    /// </summary>
    /// <typeparam name="T">节点的数据类型。</typeparam>
    public class TreeNodeCollection<T> : IEnumerable<TreeNode<T>>
    {

        /// <summary>
        /// 节点添加前引发的事件。
        /// </summary>
        public event Action NodeAdding;
        /// <summary>
        /// 节点添加后引发的事件。
        /// </summary>
        public event Action<TreeNode<T>> NodeAdded;

        /// <summary>
        /// 节点移除前引发的事件。
        /// </summary>
        public event Action<TreeNode<T>> NodeRemoving;
        /// <summary>
        /// 节点移除后引发的事件。
        /// </summary>
        public event Action NodeRemoved;

        private TreeNode<T> owner;
        private List<TreeNode<T>> collection;

        internal TreeNodeCollection(TreeNode<T> owner)
            : this()
        {
            this.owner = owner;
        }

        /// <summary>
        /// 构建一个树节点集合。
        /// </summary>
        public TreeNodeCollection()
        {
            this.collection = new List<TreeNode<T>>();
        }

        /// <summary>
        /// 构建一个树节点集合。
        /// </summary>
        /// <param name="col">节点集合。</param>
        public TreeNodeCollection(IEnumerable<TreeNode<T>> col)
            : this()
        {
            if (col != null && col.Count() > 0)
            {
                this.collection.AddRange(col);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TreeNode<T> this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return this.collection[index];
            }
        }

        /// <summary>
        /// 节点的个数。
        /// </summary>
        public int Count
        {
            get { return collection.Count; }
        }

        /// <summary>
        /// 添加一个节点。
        /// </summary>
        /// <param name="node">节点。</param>
        public void Add(TreeNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            if (this.collection.Contains(node))
            {
                return;
            }

            if (NodeAdding != null)
            {
                NodeAdding();
            }

            this.collection.Add(node);
            node.Parent = this.owner;

            if (NodeAdded != null)
            {
                NodeAdded(node);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="node"></param>
        public void Insert(int index, TreeNode<T> node)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            if (this.collection.Contains(node))
            {
                return;
            }

            if (NodeAdding != null)
            {
                NodeAdding();
            }

            this.collection.Insert(index, node);
            node.Parent = this.owner;

            if (NodeAdded != null)
            {
                NodeAdded(node);
            }
        }

        /// <summary>
        /// 移除一个节点。
        /// </summary>
        /// <param name="node">节点。</param>
        public void Remove(TreeNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            if (!this.collection.Contains(node))
            {
                return;
            }

            if (NodeRemoving != null)
            {
                NodeRemoving(node);
            }

            if (this.collection.Remove(node))
            {
                node.Parent = null;

                if (NodeRemoved != null)
                {
                    NodeRemoved();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            TreeNode<T> node = this[index];

            if (NodeRemoving != null)
            {
                NodeRemoving(node);
            }

            if (this.collection.Remove(node))
            {
                node.Parent = null;

                if (NodeRemoved != null)
                {
                    NodeRemoved();
                }
            }
        }

        /// <summary>
        /// 清空。
        /// </summary>
        public void Clear()
        {
            foreach (TreeNode<T> node in this.collection)
            {
                node.Parent = null;
                if (!node.IsLeaf)
                {
                    node.Children.Clear();
                }
            }
            this.collection.Clear();
        }

        /// <summary>
        /// 给集合中的节点排序。
        /// </summary>
        /// <param name="comparison">排序比较算法。</param>
        public void Sort(Comparison<TreeNode<T>> comparison = null)
        {
            if (comparison == null)
            {
                this.collection.Sort();
            }
            else
            {
                this.collection.Sort(comparison);
            }
        }

        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

    }

}
