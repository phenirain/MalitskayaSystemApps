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
    /// Логика взаимодействия для TexturePage.xaml
    /// </summary>
    public partial class TexturePage : Page
    {
        Frame frame;
        public TexturePage(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            DataContext = new ProductsViewModel();
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
            frame.Content = new ProductsPage(frame);
        }
    }
}
