using MalitskayaSystemWpfApp.Model.ProductsSupportModels;
using MalitskayaSystemWpfApp.Model;
using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MalitskayaSystemWpfApp.Model.OrdersSupportModels;
using MalitskayaSystemWpfApp.Model.CustomersSupportModels;
using Newtonsoft.Json;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Controls;

namespace MalitskayaSystemWpfApp.ViewModel.OrdersViewModels
{
    internal class ShippingsViewModel: BindingHelper
    {
        private ObservableCollection<ShippingsFullData> shippingsContent;
        public ObservableCollection<ShippingsFullData> ShippingsContent
        {
            get { return shippingsContent; }
            set
            {
                shippingsContent = value;
                OnPropertyChanged(nameof(ShippingsContent));
            }
        }

        private ObservableCollection<OrdersFullData> orders;
        public ObservableCollection<OrdersFullData> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        private ShippingsModel shipping;
        public ShippingsModel Shipping
        {
            get { return shipping; }
            set
            {
                shipping = value;
                OnPropertyChanged(nameof(Shipping));
            }
        }

        public BindableCommands Add { get; set; }
        public BindableCommands Edit { get; set; }
        public BindableCommands Delete { get; set; }

        private ComboBox cbx;

        public ShippingsViewModel() 
        {
            ShippingsContent = new ObservableCollection<ShippingsFullData>( 
                ApiHelper.Get<List<ShippingsFullData>>("Shippings").OrderBy(x => x.id).ToList());
            Orders = ApiHelper.Get<ObservableCollection<OrdersFullData>>("Orders");
            Add = new BindableCommands(_ => addComand());
            Edit = new BindableCommands(_ => editCommand());
            Delete = new BindableCommands(_ => deleteCommand());
            Shipping = new ShippingsModel();
            Shipping.ShippingDate = DateTime.Now.Date.ToString();
        }

        public void comboBoxHandler(object sender, RoutedEventArgs e)
        {
            cbx = sender as ComboBox;
            if (cbx.SelectedItem != null)
            {
                OrdersFullData order = cbx.SelectedItem as OrdersFullData;
                Shipping.OrderID_id = order.id;
            }
        }

        public void gridHandler(object sender, RoutedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid.SelectedItem != null)
            {
                ShippingsFullData model = grid.SelectedItem as ShippingsFullData;
                Shipping.id = model.id;
                Shipping.OrderID_id = model.OrderID_id;
                Shipping.ShippingPrice = model.ShippingPrice;
                Shipping.ShippingDate = model.ShippingDate;
                Shipping.ShippingGoodBeginTime = model.ShippingGoodBeginTime.ToString();
                Shipping.ShippingGoodEndTime = model.ShippingGoodEndTime.ToString();
                Shipping.OrderID_id = model.OrderID_id;
                if (cbx != null) cbx.Text = model.OrderID_id.ToString();
            }
        }

        public void addComand()
        {
            if (Shipping.OrderID_id != 0)
            {
                string json = JsonConvert.SerializeObject(Shipping);
                ShippingsFullData newObj =  ApiHelper.Post<List<ShippingsFullData>>(json, "Shippings")[0];
                ShippingsContent.Add(newObj);
                ShippingsContent = new ObservableCollection<ShippingsFullData>(ShippingsContent.OrderBy(x => x.id));
            } else
            {
                MessageBox.Show("Сначала нужно выбрать номер заказа");
            }
        }

        public void editCommand()
        {
            if (Shipping.id != 0)
            {
                string json = JsonConvert.SerializeObject(Shipping);
                int id = ShippingsContent.IndexOf(ShippingsContent.Where(x => x.id == Shipping.id).First());
                ShippingsFullData edited = ApiHelper.Put<List<ShippingsFullData>>(json, "Shippings")[0];
                ShippingsContent[id] = edited;
                ShippingsContent = new ObservableCollection<ShippingsFullData>(ShippingsContent.OrderBy(x => x.id));
            }
        }

        public void deleteCommand()
        {
            if (Shipping.id != 0)
            {
                bool canDelete = ApiHelper.Delete("Shippings", id: Shipping.id);
                if (canDelete)
                {
                    ShippingsContent.Remove(ShippingsContent.Where(x => x.id == Shipping.id).First());
                    ShippingsContent = new ObservableCollection<ShippingsFullData>(ShippingsContent.OrderBy(x => x.id));
                    Shipping.id = 0;
                }
            } else
            {
                MessageBox.Show("Сначала нужно выбрать доставку");
            }

        }
    }
}
