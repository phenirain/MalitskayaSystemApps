using MalitskayaSystemWpfApp.Model.ProductsSupportModels;
using MalitskayaSystemWpfApp.Model;
using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MalitskayaSystemWpfApp.Model.OrdersSupportModels;
using MalitskayaSystemWpfApp.Model.CustomersSupportModels;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace MalitskayaSystemWpfApp.ViewModel.OrdersViewModels
{
    internal class OrderDetailsViewModel: BindingHelper
    {

        private ObservableCollection<OrderDetailsFullData> detailsContent;
        public ObservableCollection<OrderDetailsFullData> DetailsContent
        {
            get { return detailsContent; }
            set
            {
                detailsContent = value;
                OnPropertyChanged(nameof(DetailsContent));
            }
        }

        private ObservableCollection<OrdersFullData> ordersContent;
        public ObservableCollection<OrdersFullData> OrdersContent
        {
            get { return ordersContent; }
            set
            {
                ordersContent = value;
                OnPropertyChanged(nameof(OrdersContent));
            }
        }

        private OrderDetailsModel detail;
        public OrderDetailsModel Detail
        {
            get { return detail; }
            set
            {
                detail = value;
                OnPropertyChanged(nameof(Detail));
            }
        }

        private OrdersModel order;
        public OrdersModel Order
        {
            get { return order; }
            set
            {
                order = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        private string totalAmount;
        public string TotalAmount
        {
            get { return totalAmount; }
            set
            {
                totalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        public BindableCommands Receipt { get; set; }
        public BindableCommands Edit { get; set; }
        public BindableCommands Delete { get; set; }
        private ComboBox cbx;

        private OrderDetailsFullData model;
        private List<ProductsFullData> products;
        private ProductsModel newProduct;


        public OrderDetailsViewModel() 
        {
            DetailsContent = new ObservableCollection<OrderDetailsFullData>(
                ApiHelper.Get<List<OrderDetailsFullData>>("OrderDetails").OrderBy(x => x.id).ToList());
            OrdersContent =  new ObservableCollection<OrdersFullData>(
                ApiHelper.Get<List<OrdersFullData>>("Orders").OrderBy(x => x.id).ToList());
            products = ApiHelper.Get<List<ProductsFullData>>("Products");
            Detail = new OrderDetailsModel();
            Receipt = new BindableCommands(_ => ReceiptCommand());
            Edit = new BindableCommands(_ => editCommand());
            Delete = new BindableCommands(_ => deleteCommand());
            Order = new OrdersModel();
            newProduct = new ProductsModel();
        }

        public void gridHandler(object sender, RoutedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid.SelectedItem != null)
            {
                model = grid.SelectedItem as OrderDetailsFullData;
                Detail.id = model.id;
                Detail.OrderID_id = model.OrderID_id;
                Detail.Articul_id = model.Articul;
                Detail.Quantity = model.Quantity;
                Detail.Amount = model.Amount;
                newProduct.Quantity = model.Quantity;
            }
        }

        public void comboBoxHandler(object sender, RoutedEventArgs e)
        {
            cbx = sender as ComboBox;
            if (cbx.SelectedItem != null)
            {
                var content = ApiHelper.Get<ObservableCollection<OrderDetailsFullData>>("OrderDetails", orderID: (cbx.SelectedItem as OrdersFullData).id);
                DetailsContent = new ObservableCollection<OrderDetailsFullData>(
                    content.Where(x => x.OrderID_id == (cbx.SelectedItem as OrdersFullData).id));
                TotalAmount = $"Общая стоимость: {DetailsContent.Sum(x => x.Amount * x.Quantity)}";
            } else
            {
                DetailsContent = new ObservableCollection<OrderDetailsFullData>(
                    ApiHelper.Get<List<OrderDetailsFullData>>("OrderDetails").OrderBy(x => x.id));
                TotalAmount = "";
            }
        }

        public void ReceiptCommand()
        {
            string Receipt = $"Заказ #{DetailsContent[0].OrderID_id}\nТовары:\n";
            foreach (var product in DetailsContent)
            {
                Receipt += $"{product.Articul}, {product.ProductName} - {product.Price} - {product.Quantity}шт: {product.Amount}\n";
            }
            var order = OrdersContent.Where(x => x.id == DetailsContent[0].OrderID_id).First();
            Receipt += $"Общая цена: {order.TotalAmount}\nПринято: {order.ReceivedAmount}\nСдача: {order.OrderChange}\nДата: {order.OrderDate}";
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\Заказ #{order.id}.txt", Receipt);
            MessageBox.Show($"Успешная выгрузка чека заказа {DetailsContent[0].OrderID_id}");
            cbx.Text = "";
            DetailsContent = new ObservableCollection<OrderDetailsFullData>(
                ApiHelper.Get<List<OrderDetailsFullData>>("OrderDetails").OrderBy(x => x.id));
            TotalAmount = "";
        }

        public void editCommand()
        {
            if (Detail.id != 0)
            {
                if (Detail.Quantity >= 0)
                {
                    string json = JsonConvert.SerializeObject(Detail);
                    int id = DetailsContent.IndexOf(DetailsContent.Where(x => x.id == Detail.id).First());
                    OrderDetailsFullData edited =  ApiHelper.Put<List<OrderDetailsFullData>>(json, "OrderDetails")[0];
                    DetailsContent[id] = edited;
                    DetailsContent = new ObservableCollection<OrderDetailsFullData>(
                    ApiHelper.Get<List<OrderDetailsFullData>>("OrderDetails").OrderBy(x => x.id));
                } else
                {
                    MessageBox.Show("Количество не может быть меньше или равно нулю");
                }
            }
        }

        public void deleteCommand()
        {
            if (Detail.id != 0)
            {
                bool canDelete = ApiHelper.Delete("OrderDetails", Detail.id);
                if (canDelete)
                {
                    DetailsContent.Remove(DetailsContent.Where(x => x.id == Detail.id).First());
                    DetailsContent = new ObservableCollection<OrderDetailsFullData>(
                    ApiHelper.Get<List<OrderDetailsFullData>>("OrderDetails").OrderBy(x => x.id));
                    Detail.id = 0;
                }
            } else
            {
                MessageBox.Show("Сначала нужно выбрать запись");
            }

        }

    }
}
