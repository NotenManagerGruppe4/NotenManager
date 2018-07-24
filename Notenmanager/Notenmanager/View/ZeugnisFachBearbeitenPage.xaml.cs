using Notenmanager.Model;
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
    /// Interaktionslogik für FachAnlegenPage.xaml
    /// </summary>
    public partial class FachAnlegenPage : Page
    {
       
        public FachAnlegenPage()
        {
            InitializeComponent();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
           

            //Ansonsten geht das Binding verloren!
            if(comboxFachart.ItemsSource == null)
                comboxFachart.ItemsSource = Enum.GetValues(typeof(Fachart));

           
        }

    }
}
