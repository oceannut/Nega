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
    public class TreeNodeModel : BaseModel, ITreeNode
    {

        #region events

        public event Action<TreeNodeModel> SelectionChanged;

        #endregion

        #region properties

        private TreeNodeModel parent;
        /// <summary>
        /// 
        /// </summary>
        public TreeNodeModel Parent
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

        private ObservableCollection<TreeNodeModel> children;
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<TreeNodeModel> Children
        {
            get { return children; }
            set 
            {
                if (children != value)
                {
                    children = value;
                    OnPropertyChanged("Children");
                }
            }
        }

        private bool isSelected;
        /// <summary>
        /// 
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set 
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");

                    if (SelectionChanged != null)
                    {
                        SelectionChanged(this);
                    }
                }
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set 
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
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
