using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LearnCode.Client
{
    public class FderivsDataGrid : DataGrid
    {
        static FderivsDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FderivsDataGrid),
                new FrameworkPropertyMetadata(typeof(FderivsDataGrid)));
        }

        #region Example TextBox

        #endregion

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

        //#region Pagination

        //public static readonly DependencyProperty CommandProperty =
        //    DependencyProperty.Register(
        //        "Command",
        //        typeof(ICommand),
        //        typeof(UserControl));

        //public ICommand Command
        //{
        //    get { return (ICommand)GetValue(CommandProperty); }
        //    set { SetValue(CommandProperty, value); }
        //}

        //#endregion
    }
}
