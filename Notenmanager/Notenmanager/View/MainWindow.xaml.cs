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
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainPageVM();
            (DataContext as MainPageVM).NavigateToPageRequest += DateiImportPage_NavigateToPageRequest;
        }

        private void DateiImportPage_NavigateToPageRequest(object sender, NavigationEventArgs e)
        {
            frMainFrame.Navigate(new DateiImportPage());

            

            //this.Height = frMainFrame.Height;
            //this.Width = frMainFrame.Width;
        }
    }
}
