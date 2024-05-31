using MalitskayaSystemWpfApp.ViewModel.CustomersViewModels;
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

namespace MalitskayaSystemWpfApp.View.Pages.CustomersPages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        Frame frame;
        public UsersPage(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            DataContext = new CustomersSupportViewModel();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new CustomersPage(frame);
        }
    }
}
