using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LearnCode.Client
{
    [TemplatePart(Name = "PART_PageTextBox", Type = typeof(TextBox))]
    public class FDataGrid : DataGrid
    {
        protected TextBox textBlockPage;

        public FDataGrid()
        {
            this.Loaded += FderivsDataGridLoaded;
        }

        ~FDataGrid()
        {
            UnregisterEvents();
        }

        private void FderivsDataGridLoaded(object sender, RoutedEventArgs e)
        {
            RegisterEvents();
        }

        #region Internal Methods

        public override void OnApplyTemplate()
        {
            textBlockPage = this.Template.FindName("PART_PageTextBox", this) as TextBox;
            base.OnApplyTemplate();
        }

        private void RegisterEvents()
        {
            textBlockPage.LostFocus += TextBoxPageLostFocus;
        }

        private void UnregisterEvents()
        {
            textBlockPage.LostFocus -= TextBoxPageLostFocus;
        }

        private void TextBoxPageLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            PageIndexCommand.Execute(textbox.Text);
        }

        #endregion

        static FDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FDataGrid),
                new FrameworkPropertyMetadata(typeof(FDataGrid)));
        }

        #region Pagination

        public static readonly DependencyProperty EnablePaginationProperty = DependencyProperty.Register(nameof(EnablePagination), typeof(bool), typeof(FDataGrid), new UIPropertyMetadata(true));
        public static readonly DependencyProperty NextPageCommandProperty = DependencyProperty.Register(nameof(NextPageCommand), typeof(ICommand), typeof(FDataGrid));
        public static readonly DependencyProperty PreviousPageCommandProperty = DependencyProperty.Register(nameof(PreviousPageCommand), typeof(ICommand), typeof(FDataGrid));
        public static readonly DependencyProperty FirstPageCommandProperty = DependencyProperty.Register(nameof(FirstPageCommand), typeof(ICommand), typeof(FDataGrid));
        public static readonly DependencyProperty LastPageCommandProperty = DependencyProperty.Register(nameof(LastPageCommand), typeof(ICommand), typeof(FDataGrid));
        public static readonly DependencyProperty PageIndexCommandProperty = DependencyProperty.Register(nameof(PageIndexCommand), typeof(ICommand), typeof(FDataGrid));

        public bool EnablePagination
        {
            get { return (bool)GetValue(EnablePaginationProperty); }
            set { SetValue(EnablePaginationProperty, value); }
        }

        public ICommand NextPageCommand
        {
            get { return (ICommand)GetValue(NextPageCommandProperty); }
            set { SetValue(NextPageCommandProperty, value); }
        }

        public ICommand PreviousPageCommand
        {
            get { return (ICommand)GetValue(PreviousPageCommandProperty); }
            set { SetValue(PreviousPageCommandProperty, value); }
        }

        public ICommand FirstPageCommand
        {
            get { return (ICommand)GetValue(FirstPageCommandProperty); }
            set { SetValue(FirstPageCommandProperty, value); }
        }

        public ICommand LastPageCommand
        {
            get { return (ICommand)GetValue(LastPageCommandProperty); }
            set { SetValue(LastPageCommandProperty, value); }
        }

        public ICommand PageIndexCommand
        {
            get { return (ICommand)GetValue(PageIndexCommandProperty); }
            set { SetValue(PageIndexCommandProperty, value); }
        }

        #endregion

        #region Pagination Information

        public static readonly DependencyProperty TotalItemsProperty = DependencyProperty.Register(nameof(TotalItems), typeof(int), typeof(FDataGrid));
        public static readonly DependencyProperty TotalPageProperty = DependencyProperty.Register(nameof(TotalPage), typeof(int), typeof(FDataGrid));
        public static readonly DependencyProperty ItemsPerPageProperty = DependencyProperty.Register(nameof(ItemsPerPage), typeof(int), typeof(FDataGrid));

        public int TotalItems
        {
            get { return (int)GetValue(TotalItemsProperty); }
            set { SetValue(TotalItemsProperty, value); }
        }

        public int TotalPage
        {
            get { return (int)GetValue(TotalPageProperty); }
            set { SetValue(TotalPageProperty, value); }
        }

        public int ItemsPerPage
        {
            get { return (int)GetValue(ItemsPerPageProperty); }
            set { SetValue(ItemsPerPageProperty, value); }
        }

        #endregion

        #region DataGrid Information

        public static readonly DependencyProperty SelectedLineProperty = DependencyProperty.Register(nameof(SelectedLine), typeof(int), typeof(FDataGrid));
        public static readonly DependencyProperty SelectedColumnProperty = DependencyProperty.Register(nameof(SelectedColumn), typeof(int), typeof(FDataGrid));

        public int SelectedLine
        {
            get { return (int)GetValue(SelectedLineProperty); }
            set { SetValue(SelectedLineProperty, value); }
        }

        public int SelectedColumn
        {
            get { return (int)GetValue(SelectedColumnProperty); }
            set { SetValue(SelectedColumnProperty, value); }
        }

        #endregion
    }
}
