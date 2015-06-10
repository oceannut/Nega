using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.WpfCommon
{

    /// <summary>
    /// 列表节点模型。
    /// </summary>
    public class ListNodeModel : BaseModel
    {

        #region events

        /// <summary>
        /// 当前节点选择变化引发的事件。
        /// </summary>
        public event Action<ListNodeModel> SelectionChanged;

        #endregion

        #region properties

        protected bool isSelected;
        /// <summary>
        /// 改变当前节点的选择，并触发选择事件。
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
        /// <summary>
        /// 
        /// </summary>
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

        #endregion

        /// <summary>
        /// 改变当前节点的选择，但不触发选择事件。
        /// </summary>
        /// <param name="value"></param>
        public void ChangeSelectionSilently(bool value)
        {
            if (isSelected != value)
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

    }

}
