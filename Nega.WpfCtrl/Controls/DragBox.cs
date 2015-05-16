using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nega.WpfCtrl
{

    /// <summary>
    /// 拖拽盒子。
    /// </summary>
    public class DragBox : ContentControl
    {

        public static readonly DependencyProperty DragObjectProperty =
            DependencyProperty.RegisterAttached(
                "DragObject",
                typeof(object),
                typeof(DragBox),
                new FrameworkPropertyMetadata(null));

        /// <summary>
        /// DragObject
        /// </summary>
        public object DragObject
        {
            get
            {
                return (object)this.GetValue(DragObjectProperty);
            }
            set
            {
                this.SetValue(DragObjectProperty, value);
            }
        }

        /// <summary>
        /// 开始位置
        /// </summary>
        private Point? dragStartPoint = null;

        /// <summary>
        /// 拖拽容器
        /// </summary>
        static DragBox()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DragBox), new FrameworkPropertyMetadata(typeof(DragBox)));
        }

        public DragBox()
        {
            this.ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(DragBox_ManipulationStarted);
        }

        void DragBox_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            if (!this.IsManipulationEnabled) return;

            DragDrop.DoDragDrop(this, this.DragObject, DragDropEffects.Copy);
            e.Handled = true;
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            this.dragStartPoint = new Point?(e.GetPosition(this));
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.dragStartPoint.HasValue)
            {
                if (e.LeftButton != MouseButtonState.Pressed)
                {
                    this.dragStartPoint = null;
                    return;
                }

                Point position = e.GetPosition(this);

                if ((SystemParameters.MinimumHorizontalDragDistance <= Math.Abs((double)(position.X - this.dragStartPoint.Value.X)))
                    || (SystemParameters.MinimumVerticalDragDistance <= Math.Abs((double)(position.Y - this.dragStartPoint.Value.Y))))
                {
                    DragDrop.DoDragDrop(this, this.DragObject, DragDropEffects.Copy);
                }

                e.Handled = true;
            }
        }

    }
}
