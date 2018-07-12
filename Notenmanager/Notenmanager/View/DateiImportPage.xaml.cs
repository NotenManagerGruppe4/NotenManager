using Notenmanager.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Notenmanager.View
{
    /// <summary>
    /// Interaktionslogik für DateiImportPage.xaml
    /// </summary>
    public partial class DateiImportPage : Page
    {
        private DateiImportPageVM _viewmodel;

        public DateiImportPage()
        {
            InitializeComponent();
            _viewmodel = DataContext as DateiImportPageVM;
            _viewmodel.MessageBoxRequest += OnMessageBoxRequest; 
        }

        private void OnMessageBoxRequest(object sender, Model.MessageBoxEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(e.MessageBoxText, e.Caption, e.MessageBoxButton, e.MessageBoxImage);
            e.ResultAction?.Invoke(result);
        }

        private void btnFileSelect_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void test()
        {
            //TEST2
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbbrechen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
