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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        Frame frame;
        public OrdersPage(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            DataContext = new OrdersViewModel();
        }

        private void PaymentMethodBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new PaymentMethodsPage(frame);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrdersStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new OrdersStatusPage(frame);
        }

        private void DGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                OrdersFullData data = DGrid.SelectedItem as OrdersFullData;
                Method.Text = data.PaymentMethodName;
                Status.Text = data.OrderStatusName;
                Datepck.Text = data.OrderDate.ToString();
            }
        }
    }
}
