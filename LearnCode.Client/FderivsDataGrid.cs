using System;
using System.CodeDom;
using System.Windows;
using System.Windows.Controls;

namespace LearnCode.Client
{
    public class FderivsDataGrid : DataGrid
    {
        static FderivsDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FderivsDataGrid),
                new FrameworkPropertyMetadata(typeof(FderivsDataGrid)));
        }

        public FderivsDataGrid()
        {
            this.Loaded += new RoutedEventHandler(FderivsDataGridLoaded);
        }

        #region Example TextBox

        public static readonly DependencyProperty EnableFullTextSearchProperty =
            DependencyProperty.Register(
                "EnableFullTextSearch",
                typeof(bool),
                typeof(FderivsDataGrid),
                new UIPropertyMetadata(false));

        public bool EnableFullTextSearch
        {
            get { return (bool)GetValue(EnableFullTextSearchProperty); }
            set { SetValue(EnableFullTextSearchProperty, value); }
        }

        #endregion

        #region Teste

        public static readonly DependencyProperty ItemsPerPageProperty = DependencyProperty.Register("ItemsPerPage", typeof(int), typeof(FderivsDataGrid), new UIPropertyMetadata(int.MaxValue));
        public static readonly DependencyProperty PageProperty = DependencyProperty.Register("Page", typeof(int), typeof(FderivsDataGrid), new UIPropertyMetadata(int.MaxValue));
        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(FderivsDataGrid), new UIPropertyMetadata(int.MaxValue));
        public static readonly DependencyProperty TotalPagesProperty = DependencyProperty.Register("TotalPages", typeof(int), typeof(FderivsDataGrid), new UIPropertyMetadata(int.MaxValue));

        public int ItemsPerPage
        {
            get { return (int)GetValue(ItemsPerPageProperty); }
            set { SetValue(ItemsPerPageProperty, value); }
        }

        public int Page
        {
            get { return (int)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        public int TotalPages
        {
            get { return (int)GetValue(TotalPagesProperty); }
            set { SetValue(TotalPagesProperty, value); }
        }

        private void FderivsDataGridLoaded(object sender, RoutedEventArgs e)
        {
            if (Template == null)
                throw new Exception("Control template not assigned");
        }

        #endregion
    }
}
