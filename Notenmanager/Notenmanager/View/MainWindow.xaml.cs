using Notenmanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Notenmanager.View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainPageVM _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = DataContext as MainPageVM;
            _viewModel.NavigateToPageRequest += OnNavigateToPageRequest;
        }

        private void OnNavigateToPageRequest(object sender, NavigationEventArgs e)
        {
            frMainFrame.Navigate(e.ZielPage);
            setMinimumWindowSize(e);
        }

        private void setMinimumWindowSize(NavigationEventArgs e)
        {
            //e.ZielPage.MinHeight = MinHeight;
            //e.ZielPage.MinWidth = MinWidth;

            //e.ZielPage.Height = Height;
            //e.ZielPage.Width = Width;
        }
    }

}
