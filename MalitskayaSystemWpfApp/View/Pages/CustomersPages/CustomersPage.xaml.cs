using MalitskayaSystemWpfApp.Model.CustomersSupportModels;
using MalitskayaSystemWpfApp.Utilities;
using MalitskayaSystemWpfApp.ViewModel.CustomersViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MalitskayaSystemWpfApp.View.Pages.CustomersPages
{
    /// <summary>
    /// Логика взаимодействия для CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        Frame frame;
        public CustomersPage(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            DataContext = new CustomersViewModel();
        }

        private void ShippingTypesBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new ShippingTypesPage(frame);
        }

        private void UsersBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new UsersPage(frame);
        }

        private void DGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                CustomersFullData data = DGrid.SelectedItem as CustomersFullData;
                shipping.Text = data.ShippingTypeName;
            }
        }
    }
}
