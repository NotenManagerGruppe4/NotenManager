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
        private MainWindowVM _mwVM;

        public MainWindow()
        {
            InitializeComponent();
            //_viewModel = DataContext as MainPageVM;
            //_viewModel.NavigateToPageRequest += OnNavigateToPageRequest;
            _mwVM = DataContext as MainWindowVM;
        }

        private void OnNavigateToPageRequest(object sender, NavigationEventArgs e)
        {
            frMainFrame.Navigate(e.ZielPage);
            setMinimumWindowSize(e);
            AdjustPageToMainFrame();
        }

        private void setMinimumWindowSize(NavigationEventArgs e)
        {
            //e.ZielPage.MinHeight = MinHeight;
            //e.ZielPage.MinWidth = MinWidth;

            //e.ZielPage.Height = Height;
            //e.ZielPage.Width = Width;
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness();
            }
        }

        public void AdjustPageToMainFrame()
        {
            _mwVM.CurrentPage.Height = this.frMainFrame.ActualHeight;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AdjustPageToMainFrame();
        }
    }

}
