using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LearnCode.Client
{
    [TemplatePart(Name = nameof(FDataGrid.ToPageTextBox), Type = typeof(TextBox))]
    [TemplatePart(Name = nameof(FDataGrid.SelectedCells), Type = typeof(TextBox))]
    [TemplatePart(Name = nameof(FDataGrid.SelectedColumnTextBox), Type = typeof(TextBox))]
    [TemplatePart(Name = nameof(FDataGrid.SelectedRowTextBox), Type = typeof(TextBox))]
    [TemplatePart(Name = nameof(FDataGrid.MainFDataGrid), Type = typeof(DataGrid))]
    public class FDataGrid : DataGrid
    {
        protected TextBox ToPageTextBox { get; set; }
        protected TextBox SelectedCellsTextBox { get; set; }
        protected TextBox SelectedColumnTextBox { get; set; }
        protected TextBox SelectedRowTextBox { get; set; }
        protected DataGrid MainFDataGrid { get; set; }

        #region Ctor

        static FDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FDataGrid), new FrameworkPropertyMetadata(typeof(FDataGrid)));
        }

        public FDataGrid()
        {
            this.Loaded += FDataGridLoaded;
        }

        ~FDataGrid()
        {
            UnregisterEvents();
        }

        #endregion

        #region Internal Methods

        private void FDataGridLoaded(object sender, RoutedEventArgs e)
        {
            if (Template == null)
                throw new InvalidOperationException("Control template not assigned.");

            RegisterEvents();
        }

        private void RegisterEvents()
        {
            ToPageTextBox.LostFocus += ToPageNotifyChanged;
            ToPageTextBox.KeyDown += ToPageNotifyChanged;
            MainFDataGrid.SelectedCellsChanged += MainFDataGridSelectedCellsChanged;
        }

        private void UnregisterEvents()
        {
            ToPageTextBox.LostFocus -= ToPageNotifyChanged;
            ToPageTextBox.KeyDown -= ToPageNotifyChanged;
            MainFDataGrid.SelectedCellsChanged -= MainFDataGridSelectedCellsChanged;
        }

        private void MainFDataGridSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            SelectedCellsTextBox.Text = MainFDataGrid.SelectedCells?.Count.ToString();
            SelectedColumnTextBox.Text = (MainFDataGrid.CurrentColumn?.DisplayIndex + 1)?.ToString();
            SelectedRowTextBox.Text = (MainFDataGrid.Items.IndexOf(MainFDataGrid.CurrentItem) + 1).ToString();
        }

        public override void OnApplyTemplate()
        {
            ToPageTextBox = Template.FindName(nameof(ToPageTextBox), this) as TextBox;
            SelectedCellsTextBox = Template.FindName(nameof(SelectedCellsTextBox), this) as TextBox;
            SelectedColumnTextBox = Template.FindName(nameof(SelectedColumnTextBox), this) as TextBox;
            SelectedRowTextBox = Template.FindName(nameof(SelectedRowTextBox), this) as TextBox;
            MainFDataGrid = Template.FindName(nameof(MainFDataGrid), this) as DataGrid;

            if (ToPageTextBox == null ||
                SelectedCellsTextBox == null ||
                SelectedColumnTextBox == null ||
                SelectedRowTextBox == null ||
                MainFDataGrid == null)
                throw new InvalidOperationException("Invalid Control template.");

            base.OnApplyTemplate();
        }

        private void ToPageNotifyChanged(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            ToPageCommand?.Execute(textbox.Text);
        }

        #endregion

        #region Pagination Command
       
        public static readonly DependencyProperty NextPageCommandProperty = DependencyProperty.Register(nameof(NextPageCommand), typeof(ICommand), typeof(FDataGrid));
        public static readonly DependencyProperty PreviousPageCommandProperty = DependencyProperty.Register(nameof(PreviousPageCommand), typeof(ICommand), typeof(FDataGrid));
        public static readonly DependencyProperty FirstPageCommandProperty = DependencyProperty.Register(nameof(FirstPageCommand), typeof(ICommand), typeof(FDataGrid));
        public static readonly DependencyProperty LastPageCommandProperty = DependencyProperty.Register(nameof(LastPageCommand), typeof(ICommand), typeof(FDataGrid));
        public static readonly DependencyProperty ToPageCommandProperty = DependencyProperty.Register(nameof(ToPageCommand), typeof(ICommand), typeof(FDataGrid));
       
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

        #region Page Information

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

        #region Enable Properties

        public static readonly DependencyProperty EnableFullTextSearchProperty = DependencyProperty.Register(nameof(EnableFullTextSearch), typeof(bool), typeof(FDataGrid), new UIPropertyMetadata(false));
        public static readonly DependencyProperty EnablePaginationProperty = DependencyProperty.Register(nameof(EnablePagination), typeof(bool), typeof(FDataGrid), new UIPropertyMetadata(true));
        public static readonly DependencyProperty EnableFDataGridStatsProperty = DependencyProperty.Register(nameof(EnableFDataGridStats), typeof(bool), typeof(FDataGrid), new UIPropertyMetadata(true));

        public bool EnableFullTextSearch
        {
            get { return (bool)GetValue(EnableFullTextSearchProperty); }
            set { SetValue(EnableFullTextSearchProperty, value); }
        }

        public bool EnablePagination
        {
            get { return (bool)GetValue(EnablePaginationProperty); }
            set { SetValue(EnablePaginationProperty, value); }
        }

        public bool EnableFDataGridStats
        {
            get { return (bool)GetValue(EnableFDataGridStatsProperty); }
            set { SetValue(EnableFDataGridStatsProperty, value); }
        }

        #endregion
    }
}
