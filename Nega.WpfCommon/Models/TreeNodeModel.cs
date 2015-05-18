using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nega.Common;

namespace Nega.WpfCommon
{

    /// <summary>
    /// 树节点模型。
    /// </summary>
    public class TreeNodeModel : ListNodeModel, ITreeNode
    {

        #region properties

        private TreeNodeModel parent;
        /// <summary>
        /// 
        /// </summary>
        public virtual TreeNodeModel Parent
        {
            get { return parent; }
            set 
            {
                if (parent != value)
                {
                    parent = value;
                    OnPropertyChanged("Parent");
                }
            }
        }

        protected ObservableCollection<TreeNodeModel> children;
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<TreeNodeModel> Children
        {
            get 
            {
                if (children == null)
                {
                    children = new ObservableCollection<TreeNodeModel>();
                }
                return children; 
            }
            set 
            {
                if (children != value)
                {
                    children = value;
                    OnPropertyChanged("Children");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ITreeNode ParentNode
        {
            get { return Parent; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITreeNode> ChildNodes
        {
            get { return Children; }
        }

        #endregion

        #region methods

        public void AddChild(TreeNodeModel child)
        {
            if (child == null)
            {
                throw new ArgumentNullException();
            }
            if (this.Children.Contains(child))
            {
                return;
            }

            this.Children.Add(child);
            child.Parent = this;
        }

        public void RemoveChild(TreeNodeModel child)
        {
            if (child == null)
            {
                throw new ArgumentNullException();
            }
            if (!this.Children.Contains(child))
            {
                return;
            }

            if (this.Children.Remove(child))
            {
                child.Parent = null;
            }
        }

        public void SelectAll()
        {
            Tree.PreorderTraverse(this,
                (e) =>
                {
                    (e as TreeNodeModel).IsSelected = true;
                });
        }

        public void UnselectAll()
        {
            Tree.PreorderTraverse(this,
                (e) =>
                {
                    (e as TreeNodeModel).IsSelected = false;
                });
        }

        public void ReverseSelect()
        {
            Tree.PreorderTraverse(this,
                (e) =>
                {
                    var node = (e as TreeNodeModel);
                    node.IsSelected = !node.IsSelected;
                });
        }

        #endregion

    }

}
