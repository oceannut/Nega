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

        public event Action<ListNodeModel> SelectionChanged;

        #endregion

        #region properties

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

    }

}
