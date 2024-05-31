using MalitskayaSystemWpfApp.Model.OrdersSupportModels;
using MalitskayaSystemWpfApp.ViewModel.OrdersViewModels;
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

namespace MalitskayaSystemWpfApp.View.Pages.OrdersPages
{
    /// <summary>
    /// Логика взаимодействия для ShippingsPage.xaml
    /// </summary>
    public partial class ShippingsPage : Page
    {
        public ShippingsPage()
        {
            InitializeComponent();
            DataContext = new ShippingsViewModel();
        }

        private void DGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                ShippingsFullData data = DGrid.SelectedItem as ShippingsFullData;
                order.Text = data.OrderID_id.ToString();
            }
        }
    }
}
