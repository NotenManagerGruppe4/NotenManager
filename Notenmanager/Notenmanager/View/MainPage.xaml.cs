using Notenmanager.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notenmanager.View
{
    /// <summary>
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private MainPageVM _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = DataContext as MainPageVM;

            SetBackground();
        }

        private void SetBackground()
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("../../Resources/Add_Icon.PNG", UriKind.Relative));
            btnFachAnlegen.Background = brush;
            btnFachAnlegen.Background.Opacity = 0.25;
        }
    }
}
