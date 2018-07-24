using Notenmanager.ViewModel;
using Notenmanager.ViewModel.Tools;
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

        private ProgressBar pb;

        public ProgressBar Pb
        {
            get
            {
                return pb;
            }

            set
            {
                pb = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            //_viewModel = DataContext as MainPageVM;
            //_viewModel.NavigateToPageRequest += OnNavigateToPageRequest;
            _mwVM = DataContext as MainWindowVM;
            Navigator.Instance.PageChanged += Instance_PageChanged;
        }

        private void Instance_PageChanged(object sender, EventArgs e)
        {
            SetSizeProperties((App.Current.FindResource("MainWindowVM") as MainWindowVM).CurrentPage);
        }
        
        private void SetSizeProperties(Page p)
        {
            // Sollte die Größe des Fensters auf den Inhalt anpassen
           /* MinHeight = p.MinHeight+120;
            MinWidth = p.MinWidth+20;
            MaxWidth = p.MaxWidth;
            MaxHeight = p.MaxHeight;*/
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

        public void showProgressBar(bool b)
        {
            if (b)
            {
                pbLoading.Visibility = Visibility.Visible;
            }
                
            
            else
                pbLoading.Visibility = Visibility.Hidden;
        }
        
    }

}
