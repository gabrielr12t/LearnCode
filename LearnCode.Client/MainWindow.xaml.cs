using LearnCode.Client.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace LearnCode.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CountryViewModel viewModel = new CountryViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            (viewModel.LoadCommand as ICommand).Execute(null);
        }
    }
}
