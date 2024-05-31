using MalitskayaSystemWpfApp.View.Pages;
using MalitskayaSystemWpfApp.View.Pages.OrdersPages;
using MalitskayaSystemWpfApp.View.Pages.CustomersPages;
using System.Windows;
using MalitskayaSystemWpfApp.View.Pages.SupportPages;
using MalitskayaSystemWpfApp.Utilities;
using System.IO;
using MalitskayaSystemWpfApp.ViewModel.OrdersViewModels;
using System;


namespace MalitskayaSystemWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            Closed += deleteOrder;
        }

        private void deleteOrder(object sender = null, EventArgs e = null)
        {
            if (SupportViewModels.id != 0)
            {
                ApiHelper.Delete("Orders", id: SupportViewModels.id);
                SupportViewModels.id = 0;
            }
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            deleteOrder();
            MainFrame.Content = new ProductsPage(MainFrame);
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            deleteOrder();
            MainFrame.Content = new CustomersPage(MainFrame);
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            deleteOrder();
            MainFrame.Content = new OrdersPage(MainFrame);
        }

        private void OrderDetails_Click(object sender, RoutedEventArgs e)
        {
            deleteOrder();
            MainFrame.Content = new OrderDetailsPage();
        }

        private void Shipping_Click(object sender, RoutedEventArgs e)
        {
            deleteOrder();
            MainFrame.Content = new ShippingsPage();
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            deleteOrder();
            MainFrame.Content = new NewOrderPage();
        }


        private void WriteOffBtn_Click(object sender, RoutedEventArgs e)
        {
            deleteOrder();
            MainFrame.Content = new WriteOffPage();
        }
    }
}
