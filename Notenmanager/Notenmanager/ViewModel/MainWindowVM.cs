using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Notenmanager.ViewModel
{
    class MainWindowVM : BaseViewModel
    {
        public Page _currentPage;

        public MainWindowVM()
        {
            CurrentPage = App.Current.FindResource("MainPage") as Page;
        }

        public Page CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                SetValue(ref _currentPage, value);
            }
        }
    }
}
