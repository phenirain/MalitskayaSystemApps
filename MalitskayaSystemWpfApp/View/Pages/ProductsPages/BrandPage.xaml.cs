using MalitskayaSystemWpfApp.ViewModel.ProductsViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MalitskayaSystemWpfApp.View.Pages.ProductsPages
{
    /// <summary>
    /// Логика взаимодействия для BrandPage.xaml
    /// </summary>
    public partial class BrandPage : Page
    {
        Frame frame;
        public BrandPage(Frame frame)
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
