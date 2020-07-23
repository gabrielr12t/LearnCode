using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LearnCode.Client
{
    [TemplatePart(Name = "PART_PageTextBox", Type = typeof(TextBox))]
    public class FDataGrid : DataGrid
    {
        protected TextBox PageIndexTextBox { get; set; }

        #region Ctor

        static FDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FDataGrid),
                new FrameworkPropertyMetadata(typeof(FDataGrid)));
        }

        public FDataGrid()
        {
            this.Loaded += FderivsDataGridLoaded;
        }

        ~FDataGrid()
        {
            UnregisterEvents();
        }

        #endregion

        #region Internal Methods

        private void FderivsDataGridLoaded(object sender, RoutedEventArgs e)
        {
            RegisterEvents();
        }

        public override void OnApplyTemplate()
        {
            PageIndexTextBox = this.Template.FindName("PART_PageTextBox", this) as TextBox;
            base.OnApplyTemplate();
        }

        private void RegisterEvents()
        {
            PageIndexTextBox.LostFocus += PageIndexNotify;
            PageIndexTextBox.KeyDown += PageIndexNotify;
        }

        private void UnregisterEvents()
        {
            PageIndexTextBox.LostFocus -= PageIndexNotify;
            PageIndexTextBox.KeyDown -= PageIndexNotify;
        }

        private void PageIndexNotify(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            ToPageCommand?.Execute(textbox.Text);
        }

        #endregion

        #region Pagination Command

        public static readonly DependencyProperty EnablePaginationProperty = DependencyProperty.Register(nameof(EnablePagination), typeof(bool), typeof(FDataGrid), new UIPropertyMetadata(true));
        public static readonly DependencyProperty NextPageCommandProperty = DependencyProperty.Register(nameof(NextPageCommand), typeof(ICommand), typeof(FDataGrid));
        public static readonly DependencyProperty PreviousPageCommandProperty = DependencyProperty.Register(nameof(PreviousPageCommand), typeof(ICommand), typeof(FDataGrid));
        public static readonly DependencyProperty FirstPageCommandProperty = DependencyProperty.Register(nameof(FirstPageCommand), typeof(ICommand), typeof(FDataGrid));
        public static readonly DependencyProperty LastPageCommandProperty = DependencyProperty.Register(nameof(LastPageCommand), typeof(ICommand), typeof(FDataGrid));
        public static readonly DependencyProperty ToPageCommandProperty = DependencyProperty.Register(nameof(ToPageCommand), typeof(ICommand), typeof(FDataGrid));

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

        public ICommand ToPageCommand
        {
            get { return (ICommand)GetValue(ToPageCommandProperty); }
            set { SetValue(ToPageCommandProperty, value); }
        }

        #endregion

        #region Pagination Information

        public static readonly DependencyProperty TotalItemsProperty = DependencyProperty.Register(nameof(TotalItems), typeof(int), typeof(FDataGrid));
        public static readonly DependencyProperty TotalPagesProperty = DependencyProperty.Register(nameof(TotalPages), typeof(int), typeof(FDataGrid));
        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register(nameof(CurrentPage), typeof(int), typeof(FDataGrid));

        public int TotalItems
        {
            get { return (int)GetValue(TotalItemsProperty); }
            set { SetValue(TotalItemsProperty, value); }
        }

        public int TotalPages
        {
            get { return (int)GetValue(TotalPagesProperty); }
            set { SetValue(TotalPagesProperty, value); }
        }

        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        #endregion
    }
}
