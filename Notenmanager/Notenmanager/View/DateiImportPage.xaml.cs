using Notenmanager.ViewModel;
using System.Diagnostics;
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
            _viewmodel.NavigationRequest += OnNavigationRequest;
            _viewmodel.MessageBoxRequest += OnMessageBoxRequest; 
        }

        private void OnNavigationRequest(object sender, NavigationEventArgs e)
        {
            if(this.NavigationService.CanGoBack)
                this.NavigationService.GoBack();
        }

        private void OnMessageBoxRequest(object sender, Model.MessageBoxEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(e.MessageBoxText, e.Caption, e.MessageBoxButton, e.MessageBoxImage);
            e.ResultAction?.Invoke(result);
        }
    }
}
