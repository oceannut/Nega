using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nega.WpfCtrl
{

    public class PagingEventArgs : EventArgs
    {
        public int PageIndex { get; set; }
    }

    public delegate void PagingEventHandler(object sender, PagingEventArgs e);

    public class Pagination : Control
    {

        public event PagingEventHandler PageIndexChanged;

        private static Style pageButtonStyle;

        private Panel pageButtonPanel;
        private int startPageIndex;
        private readonly object lockObj = new object();

        static Pagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination), new FrameworkPropertyMetadata(typeof(Pagination)));
            try
            {
                pageButtonStyle = System.Windows.Application.Current.Resources["PageButtonStyle"] as Style;
            }
            catch { }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            pageButtonPanel = GetTemplateChild("PageButtonPanel") as StackPanel;
        }

        #region commands

        private ICommand firstPageCommand;
        public ICommand FirstPageCommand
        {
            get
            {
                if (firstPageCommand == null)
                {
                    firstPageCommand = new RelayCommand<object>
                        (
                        (object obj) =>
                        {
                            PageIndex = 0;
                            startPageIndex = 0;
                            BuildPageButtons();
                        },
                        (object obj) =>
                        {
                            return (PageIndex > 0);
                        }
                        );
                }
                return firstPageCommand;
            }
        }

        private ICommand prevPageCommand;
        public ICommand PrevPageCommand
        {
            get
            {
                if (prevPageCommand == null)
                {
                    prevPageCommand = new RelayCommand<object>
                        (
                        (object obj) =>
                        {
                            PageIndex = PageIndex - 1;
                            startPageIndex = startPageIndex > 0 ? startPageIndex - 1 : 0;
                            BuildPageButtons();
                        },
                        (object obj) =>
                        {
                            return ((PageIndex - 1) >= 0);
                        }
                        );
                }
                return prevPageCommand;
            }
        }

        private ICommand lastPageCommand;
        public ICommand LastPageCommand
        {
            get
            {
                if (lastPageCommand == null)
                {
                    lastPageCommand = new RelayCommand<object>
                        (
                        (object obj) =>
                        {
                            PageIndex = PageCount - 1;
                            startPageIndex = PageCount - 5 > 0 ? PageCount - 5 : 0;
                            BuildPageButtons();
                        },
                        (object obj) =>
                        {
                            return (PageIndex < (PageCount - 1));
                        }
                        );
                }
                return lastPageCommand;
            }
        }

        private ICommand nextPageCommand;
        public ICommand NextPageCommand
        {
            get
            {
                if (nextPageCommand == null)
                {
                    nextPageCommand = new RelayCommand<object>
                        (
                        (object obj) =>
                        {
                            PageIndex = PageIndex + 1;
                            startPageIndex = startPageIndex + 5 < PageCount ? startPageIndex + 1 : startPageIndex;
                            BuildPageButtons();
                        },
                        (object obj) =>
                        {
                            return ((PageIndex + 1) < PageCount);
                        }
                        );
                }
                return nextPageCommand;
            }
        }

        private ICommand pageCommand;
        public ICommand PageCommand
        {
            get
            {
                if (pageCommand == null)
                {
                    pageCommand = new RelayCommand<int>
                        (
                        (int index) =>
                        {
                            PageIndex = index;
                            foreach (var child in pageButtonPanel.Children)
                            {
                                ToggleButton button = child as ToggleButton;
                                if ((int)button.CommandParameter == index)
                                {
                                    button.IsChecked = true;
                                }
                                else
                                {
                                    button.IsChecked = false;
                                }
                            }
                        }
                        );
                }
                return pageCommand;
            }
        }

        private ICommand gotoPageCommand;
        public ICommand GotoPageCommand
        {
            get
            {
                if (gotoPageCommand == null)
                {
                    gotoPageCommand = new RelayCommand<string>
                        (
                        (string index) =>
                        {
                            if (!string.IsNullOrWhiteSpace(index))
                            {
                                try
                                {
                                    PageIndex = Convert.ToInt32(index) - 1;
                                    startPageIndex = PageIndex;
                                    BuildPageButtons();
                                }
                                catch { }
                            }
                        },
                        (string index) =>
                        {
                            if (!string.IsNullOrWhiteSpace(index))
                            {
                                try
                                {
                                    int i = Convert.ToInt32(index);
                                    return (i > 0 && i <= PageCount);
                                }
                                catch { }
                            }
                            return false;
                        }
                        );
                }
                return gotoPageCommand;
            }
        }

        #endregion

        #region TotalCount

        public int TotalCount
        {
            get { return (int)GetValue(TotalCountProperty); }
            set { SetValue(TotalCountProperty, value); }
        }

        public static readonly DependencyProperty TotalCountProperty = DependencyProperty.Register(
            "TotalCount",
            typeof(int),
            typeof(Pagination),
            new PropertyMetadata(0, new PropertyChangedCallback(OnTotalCountChanged)));

        private static void OnTotalCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pagination)d).OnTotalCountChanged(e);
        }

        protected virtual void OnTotalCountChanged(DependencyPropertyChangedEventArgs e)
        {
            Reset();
            BuildPageButtons();
        }

        #endregion

        #region PageSize

        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register(
            "PageSize",
            typeof(int),
            typeof(Pagination),
            new PropertyMetadata(20, new PropertyChangedCallback(OnPageSizeChanged)));

        private static void OnPageSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pagination)d).OnPageSizeChanged(e);
        }

        protected virtual void OnPageSizeChanged(DependencyPropertyChangedEventArgs e)
        {
            Reset();
            BuildPageButtons();
        }

        #endregion

        #region PageCount

        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            private set { SetValue(PageCountProperty, value); }
        }

        public static readonly DependencyProperty PageCountProperty = DependencyProperty.Register(
            "PageCount",
            typeof(int),
            typeof(Pagination),
            new PropertyMetadata(0, new PropertyChangedCallback(OnPageCountChanged)));

        private static void OnPageCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pagination)d).OnPageCountChanged(e);
        }

        protected virtual void OnPageCountChanged(DependencyPropertyChangedEventArgs e)
        {
            
        }

        #endregion

        #region PageIndex

        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            private set { SetValue(PageIndexProperty, value); }
        }

        public static readonly DependencyProperty PageIndexProperty = DependencyProperty.Register(
            "PageIndex",
            typeof(int),
            typeof(Pagination),
            new PropertyMetadata(-1, new PropertyChangedCallback(OnPageIndexChanged)));

        private static void OnPageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pagination)d).OnPageIndexChanged(e);
        }

        protected virtual void OnPageIndexChanged(DependencyPropertyChangedEventArgs e)
        {
            if (PageIndexChanged != null)
            {
                PageIndexChanged(this, 
                    new PagingEventArgs
                    {
                        PageIndex = PageIndex
                    });
            }
        }

        #endregion

        #region label

        public string GotoText
        {
            get { return (string)GetValue(GotoTextProperty); }
            set { SetValue(GotoTextProperty, value); }
        }

        public static readonly DependencyProperty GotoTextProperty = DependencyProperty.Register(
            "GotoText",
            typeof(string),
            typeof(Pagination),
            new PropertyMetadata("跳到"));

        public string TotalCountText
        {
            get { return (string)GetValue(TotalCountProperty); }
            set { SetValue(TotalCountProperty, value); }
        }

        public static readonly DependencyProperty TotalCountTextProperty = DependencyProperty.Register(
            "TotalCountText",
            typeof(string),
            typeof(Pagination),
            new PropertyMetadata("总数: "));

        public string CurrentPageText
        {
            get { return (string)GetValue(CurrentPageTextProperty); }
            set { SetValue(CurrentPageTextProperty, value); }
        }

        public static readonly DependencyProperty CurrentPageTextProperty = DependencyProperty.Register(
            "CurrentPageText",
            typeof(string),
            typeof(Pagination),
            new PropertyMetadata("当前页: "));

        #endregion

        private void Reset()
        {
            if (TotalCount > 0)
            {
                PageIndex = 0;
                PageCount = (int)Math.Ceiling((double)TotalCount / PageSize);
            }
            else
            {
                PageIndex = -1;
                PageCount = 0;
            }
        }

        private void BuildPageButtons()
        {
            if (PageCount > 0)
            {
                lock (lockObj)
                {
                    if (pageButtonPanel == null)
                    {
                        return;
                    }
                    pageButtonPanel.Children.Clear();
                    int end = PageCount - 5 > startPageIndex ? startPageIndex + 5 : PageCount;
                    for (int i = startPageIndex; i < end; i++)
                    {
                        ToggleButton button = new ToggleButton
                            {
                                Content = (i + 1).ToString(),
                                Style = pageButtonStyle,
                                Command = PageCommand,
                                CommandParameter = i
                            };
                        if (i == PageIndex)
                        {
                            button.IsChecked = true;
                        }
                        else
                        {
                            button.IsChecked = false;
                        }
                        if (pageButtonPanel == null)
                        {
                            return;
                        }
                        pageButtonPanel.Children.Add(button);
                    }
                }
            }
        }

    }

}
