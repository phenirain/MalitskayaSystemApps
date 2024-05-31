using MalitskayaSystemWpfApp.Model.ProductsSupportModels;
using MalitskayaSystemWpfApp.Utilities;
using MalitskayaSystemWpfApp.View.Pages.ProductsPages;
using MalitskayaSystemWpfApp.ViewModel.ProductsViewModels;
using Newtonsoft;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MalitskayaSystemWpfApp.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        Frame frame;
        public ProductsPage(Frame frame)
        {
            InitializeComponent();
            DataContext = new ProductsViewModel();
            this.frame = frame;
        }

        private void BrandBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new BrandPage(frame);
        }

        private void ProductTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new ProductTypePage(frame);
        }

        private void TextureBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new TexturePage(frame);
        }

        private void VolumeTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new VolumeTypePage(frame);
        }

        private void DGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                ProductsFullData data = DGrid.SelectedItem as ProductsFullData;
                brand.Text = data.BrandTypeName;
                type.Text = data.ProductTypeName;
                texture.Text = data.TextureTypeName;
                volume.Text = data.ProductVolumeTypeName;
            }
        }

    }
}
