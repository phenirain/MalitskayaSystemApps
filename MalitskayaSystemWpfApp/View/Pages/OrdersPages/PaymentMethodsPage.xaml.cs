using MalitskayaSystemWpfApp.ViewModel.OrdersViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MalitskayaSystemWpfApp.View.Pages.OrdersPages
{
    /// <summary>
    /// Логика взаимодействия для PaymentMethodsPage.xaml
    /// </summary>
    public partial class PaymentMethodsPage : Page
    {
        Frame frame;
        public PaymentMethodsPage(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            DataContext = new OrdersViewModel();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new OrdersPage(frame);
        }
    }
}
