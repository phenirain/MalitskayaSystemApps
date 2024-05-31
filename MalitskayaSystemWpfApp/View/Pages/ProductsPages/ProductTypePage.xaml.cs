using MalitskayaSystemWpfApp.ViewModel.ProductsViewModels;
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

namespace MalitskayaSystemWpfApp.View.Pages.ProductsPages
{
    /// <summary>
    /// Логика взаимодействия для ProductTypePage.xaml
    /// </summary>
    public partial class ProductTypePage : Page
    {
        Frame frame;
        public ProductTypePage(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            DataContext = new ProductsViewModel();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new ProductsPage(frame);
        }
    }
}
