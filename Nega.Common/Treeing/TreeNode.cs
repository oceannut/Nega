using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    /// <summary>
    /// 定义一类简单的树形结构的节点，具有唯一的或者无父子节点，以及0个或多个孩子节点。
    /// </summary>
    /// <typeparam name="T">节点的数据类型。</typeparam>
    public class TreeNode<T> : ITreeNode
    {

        /// <summary>
        /// 父节点变化前引发的事件。
        /// </summary>
        public event Action<TreeNode<T>, TreeNode<T>> ParentChanging;
        /// <summary>
        /// 父节点变化后引发的事件。
        /// </summary>
        public event Action<TreeNode<T>> ParentChanged;

        private TreeNode<T> parent;
        private TreeNodeCollection<T> children;

        /// <summary>
        /// 节点的数据。
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 是否是叶子节点，即无孩子节点。
        /// </summary>
        public bool IsLeaf
        {
            get
            {
                return (children == null || children.Count == 0);
            }
        }

        /// <summary>
        /// 父子节点。
        /// </summary>
        public TreeNode<T> Parent
        {
            get { return parent; }
            internal set
            {
                if (parent != value)
                {
                    if (ParentChanging != null)
                    {
                        ParentChanging(parent, value);
                    }

                    parent = value;

                    if (ParentChanged != null)
                    {
                        ParentChanged(parent);
                    }
                }
            }
        }

        /// <summary>
        /// 孩子节点。
        /// </summary>
        public TreeNodeCollection<T> Children
        {
            get
            {
                if (children == null)
                {
                    children = new TreeNodeCollection<T>(this);
                }
                return children;
            }
            internal set { children = value; }
        }


        public ITreeNode ParentNode
        {
            get { return Parent; }
        }

        public IEnumerable<ITreeNode> ChildNodes
        {
            get { return Children; }
        }

    }

}
