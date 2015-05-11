using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Nega.WpfCommon;

namespace Nega.WpfCtrl
{
    public class CloseableTabItem : TabItem
    {

        public event Action<CloseableTabItem> OnClosing;
        public event Action<CloseableTabItem> OnClosed;

        public CloseableTabItem()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(CloseableTabItem),
            //    new FrameworkPropertyMetadata(typeof(CloseableTabItem)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.DataContext = this;

        }

        private ICommand closeCommand;
        /// <summary>
        /// 关闭选项卡的命令。
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand<object>
                        (
                        (object obj) =>
                        {
                            if (OnClosing != null)
                            {
                                OnClosing(this);
                            }
                            ((TabControl)this.Parent).Items.Remove(this);
                            if (OnClosed != null)
                            {
                                OnClosed(this);
                            }
                        },
                        (object obj) =>
                        {
                            return true;
                        }
                        );
                }
                return closeCommand;
            }
        }

        /// <summary>
        /// 关闭按钮显示的ToolTip信息。
        /// </summary>
        public string CloseButtonToolTip
        {
            get { return GetValue(CloseButtonToolTipProperty) as string; }
            set { SetValue(CloseButtonToolTipProperty, value); }
        }

        /// <summary>
        /// ToolTip Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CloseButtonToolTipProperty =
            DependencyProperty.Register(
                "CloseButtonToolTip",
                typeof(string),
                typeof(CloseableTabItem),
                new PropertyMetadata("To close tab", null));

    }
}
