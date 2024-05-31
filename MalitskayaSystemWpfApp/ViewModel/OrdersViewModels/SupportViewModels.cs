using MalitskayaSystemWpfApp.Model.OrdersSupportModels;
using MalitskayaSystemWpfApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MalitskayaSystemWpfApp.Utilities;
using MalitskayaSystemWpfApp.Model.ProductsSupportModels;
using MalitskayaSystemWpfApp.Model.CustomersSupportModels;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Runtime.CompilerServices;

namespace MalitskayaSystemWpfApp.ViewModel.OrdersViewModels
{
    internal class SupportViewModels: BindingHelper
    {
        #region collections
        private ObservableCollection<ProductsFullData> productsContent;
        public ObservableCollection<ProductsFullData> ProductsContent
        {
            get { return productsContent; }
            set
            {
                productsContent = value;
                OnPropertyChanged(nameof(ProductsContent));
            }
        }

        private ObservableCollection<OrderDetailsFullData> orderCost;
        public ObservableCollection<OrderDetailsFullData> OrderCost
        {
            get { return orderCost; }
            set
            {
                orderCost = value;
                OnPropertyChanged(nameof(OrderCost));
            }
        }

        private List<CustomersFullData> customersContent;
        public List<CustomersFullData> CustomersContent
        {
            get { return customersContent; }
            set
            {
                customersContent = value;
                OnPropertyChanged(nameof(CustomersContent));
            }
        }

        private List<PaymentMethodsModel> payContent;
        public List<PaymentMethodsModel> PayContent
        {
            get { return payContent; }
            set
            {
                payContent = value;
                OnPropertyChanged(nameof(PayContent));
            }
        }

        #endregion
        #region newObj
        private ProductsModel product;
        private int offvalue;
        public int OffValue
        {
            get { return offvalue; }
            set { 
                offvalue = value;
                OnPropertyChanged(nameof(OffValue));
            }
        }

        private int addValue;
        public int AddValue
        {
            get { return addValue; }
            set
            {
                addValue = value;
                OnPropertyChanged(nameof(AddValue));
            }
        }
        private OrdersModel order;
        public OrdersModel Order
        {
            get { return order; }
            set
            {
                order = value;
                OnPropertyChanged();
            }
        }

        private string orderPrice;
        public string OrderPrice
        {
            get { return orderPrice; }
            set 
            {
                orderPrice = value;
                OnPropertyChanged(nameof(OrderPrice));
            }
        }

        public static int id;

        #endregion

        public BindableCommands Off {  get; set; }
        public BindableCommands Add {  get; set; }
        public BindableCommands ToCart { get; set; }
        public BindableCommands ToStock { get; set; }
        public BindableCommands Save {  get; set; }

        private List<BrandTypesModel> brandTypes;
        private List<TextureTypesModel> textureTypes;
        private List<ProductVolumeTypesModel> productVolumeTypes;
        private List<ProductTypesModel> productTypes;

        private DataGrid stock;
        private DataGrid cost;
        private ComboBox CustCmb;
        private ComboBox MethodCmb;
        private CustomersFullData customer;
        private List<OrderDetailsModel> details;

        public SupportViewModels(DataGrid stock)
        {
            if (stock != null)
            {
                ProductsContent = new ObservableCollection<ProductsFullData>(
                    ApiHelper.Get<List<ProductsFullData>>("Products").Where(x => x.Quantity > 0).OrderBy(x => x.Articul));
                this.stock = stock;
            } else
            {
                ProductsContent = new ObservableCollection<ProductsFullData>(
                    ApiHelper.Get<List<ProductsFullData>>("Products").OrderBy(x => x.Articul));
            }
            PayContent = ApiHelper.Get<List<PaymentMethodsModel>>("PaymentMethods");
            CustomersContent = ApiHelper.Get<List<CustomersFullData>>("Customers");
            brandTypes = ApiHelper.Get<List<BrandTypesModel>>("BrandTypes");
            textureTypes = ApiHelper.Get<List<TextureTypesModel>>("TextureTypes");
            productVolumeTypes = ApiHelper.Get<List<ProductVolumeTypesModel>>("ProductVolumeTypes");
            productTypes = ApiHelper.Get<List<ProductTypesModel>>("ProductTypes");
            product = new ProductsModel();
            ToCart = new BindableCommands(_ => addToCart());
            ToStock = new BindableCommands(_ => minusOfCart());
            Save = new BindableCommands(_ => saveOrder());
            Off = new BindableCommands(_ => productOff());
            Add = new BindableCommands(_ => productAdd());
            Order = new OrdersModel();
            OrderCost = new ObservableCollection<OrderDetailsFullData>();
        }

        public void comboBoxHandler(object sender, RoutedEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx.SelectedItem != null)
            {
                if (cbx.Name == "MethodCbx")
                {
                    Order.PaymentMethodID_id = (cbx.SelectedItem as PaymentMethodsModel).id;
                    MethodCmb = cbx;
                }
                else
                {
                    CustCmb = cbx;
                    customer = cbx.SelectedItem as CustomersFullData;
                    Order.CustomerID_id = customer.id;
                    DataGridTextColumn column = (DataGridTextColumn)stock.Columns[2];
                    if (customer.isProfi) column.Binding = new Binding("WholeSalePrice");
                    else column.Binding = new Binding("RetailPrice");
                }
            }
        }

        public void gridHandler(object sender, RoutedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid.SelectedItem != null)
            {
                if (grid.Name != "stock") cost = grid;
            }
        }
        public void addToCart()
        {
            if (stock.SelectedItem != null)
            {
                if (Order.CustomerID_id == 0)
                {
                    MessageBox.Show("Сначала нужно выбрать покупателя");
                } else if (Order.PaymentMethodID_id == 0)
                {
                    MessageBox.Show("Сначала нужно выбрать способ оплаты");
                } else
                {
                    var selected = (stock.SelectedItem as ProductsFullData);
                    if (selected.Quantity != 0)
                    {
                        if (Order.id == 0)
                        {
                            Order.OrderDescription = "-";
                            Order.TotalAmount = 0;
                            Order.ReceivedAmount = 0;
                            Order.OrderChange = 0;
                            Order.OrderStatusID_id = 3;
                            Order.OrderDate = DateTime.Now.ToString();
                            string json = JsonConvert.SerializeObject(Order);
                            OrdersFullData newOrder = ApiHelper.Post<List<OrdersFullData>>(json, "Orders")[0];
                            Order.id = newOrder.id;
                            id = Order.id;
                        }
                        bool isExist = false;
                        int detailID = 0;
                        OrderDetailsModel detail = new OrderDetailsModel();
                        if (OrderCost.Count != 0)
                        {
                            if (OrderCost.Where(x => x.OrderID_id == Order.id && x.Articul == selected.Articul).Any())
                            {
                                var detailFull = OrderCost.Where(x => x.OrderID_id == Order.id && x.Articul == selected.Articul).First();
                                detail.id = detailFull.id;
                                detail.Quantity = detailFull.Quantity + 1;
                                isExist = true;
                                detailID = OrderCost.IndexOf(detailFull);
                            }
                            else
                            {
                                detail.Quantity += 1;
                            }
                        }
                        else
                        {
                            detail.Quantity += 1;
                        }
                        detail.OrderID_id = Order.id;
                        detail.Articul_id = selected.Articul;
                        detail.Amount = customer.isProfi ? selected.WholeSalePrice * detail.Quantity : selected.RetailPrice * detail.Quantity;
                        string detailJson = JsonConvert.SerializeObject(detail);
                        if (isExist)
                        {
                            OrderDetailsFullData edited = ApiHelper.Put<List<OrderDetailsFullData>>(detailJson, "OrderDetails")[0];
                            OrderCost[detailID] = edited;
                        }
                        else
                        {
                            OrderDetailsFullData newObj = ApiHelper.Post<List<OrderDetailsFullData>>(detailJson, "OrderDetails")[0];
                            OrderCost.Add(newObj);
                        }
                        Order.TotalAmount = OrderCost.Sum(x => x.Price * x.Quantity);
                        OrderPrice = $"Стоимость: {Order.TotalAmount}";
                        ProductsContent = new ObservableCollection<ProductsFullData>(
                            ApiHelper.Get<List<ProductsFullData>>("Products").Where(x => x.Quantity > 0).OrderBy(x => x.Articul));
                    } else
                    {
                        MessageBox.Show("Количество выбранного товара на складе меньше 0");
                    }
                }
            } else
            {
                MessageBox.Show("Сначала нужно выбрать товар");
            }
        }

        public void minusOfCart()
        {
            if (cost.SelectedItem != null)
            {
                if (Order.CustomerID_id == 0)
                {
                    MessageBox.Show("Сначала нужно выбрать покупателя");
                }
                else if (Order.PaymentMethodID_id == 0)
                {
                    MessageBox.Show("Сначала нужно выбрать способ оплаты");
                }
                else
                {
                    OrderDetailsFullData selected = cost.SelectedItem as OrderDetailsFullData;
                    int detailID = cost.SelectedIndex;
                    var selectedProduct = ProductsContent.Where(x => x.Articul == selected.Articul).First();
                    OrderDetailsModel orderDetails = new OrderDetailsModel();
                    orderDetails.id = selected.id;
                    orderDetails.Quantity = selected.Quantity - 1;
                    orderDetails.OrderID_id = selected.OrderID_id;
                    orderDetails.Articul_id = selected.Articul;
                    string json = JsonConvert.SerializeObject(orderDetails);
                    List<OrderDetailsFullData> newObj = ApiHelper.Put<List<OrderDetailsFullData>>(json, "OrderDetails");
                    if (newObj.Count == 0)
                    {
                        OrderCost.RemoveAt(detailID);
                    } else
                    {
                        OrderCost[detailID] = newObj[0];
                    }
                    if (OrderCost.Count == 0)
                    {
                        Order.TotalAmount = 0;
                    } else
                    {
                        Order.TotalAmount = OrderCost.Sum(x => x.Price * x.Quantity);
                    }
                    OrderPrice = $"Стоимость: {Order.TotalAmount}";
                    ProductsContent = new ObservableCollection<ProductsFullData>(
                        ApiHelper.Get<List<ProductsFullData>>("Products").OrderBy(x => x.Articul));
                }
             } else MessageBox.Show("Сначала нужно выбрать товар");
        }

        public void saveOrder()
        {
            if (Order.ReceivedAmount < Order.TotalAmount)
            {
                MessageBox.Show("Нельзя получить меньше чем стоимость(");
            } else if (OrderCost.Count == 0)
            {
                MessageBox.Show("Нельзя создать пустой заказ");
            }
            {
                Order.OrderChange = Order.ReceivedAmount - Order.TotalAmount;
                Order.OrderStatusID_id = 2;
                string orderJson = JsonConvert.SerializeObject(Order);
                ApiHelper.Put<List<OrdersFullData>>(orderJson, "Orders");
                Order = new OrdersModel();
                OrderPrice = "";
                OrderCost = null;
                CustCmb.Text = null;
                MethodCmb.Text = null;
                id = 0;
            }
        }


        public void ProductHandler(object sender, RoutedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid.SelectedItem != null)
            {
                ProductsFullData model = grid.SelectedItem as ProductsFullData;
                product.Articul = model.Articul;
                product.ProductName = model.ProductName;
                product.ProductDescription = model.ProductDescription;
                if (brandTypes.Where(x => x.BrandTypeName == model.BrandTypeName).Any())
                    product.BrandTypeID_id = brandTypes.Where(x => x.BrandTypeName == model.BrandTypeName).First().id;
                if (productTypes.Where(x => x.ProductTypeName == model.ProductTypeName).Any())
                    product.ProductTypeID_id = productTypes.Where(x => x.ProductTypeName == model.ProductTypeName).First().id;
                if (textureTypes.Where(x => x.TextureTypeName == model.TextureTypeName).Any())
                    product.TextureTypeID_id = textureTypes.Where(x => x.TextureTypeName == model.TextureTypeName).First().id;
                product.ProductVolume = model.ProductVolume;
                if (productVolumeTypes.Where(x => x.ProductVolumeTypeName == model.ProductVolumeTypeName).Any())
                    product.ProductVolumeTypeID_id = productVolumeTypes.Where(x => x.ProductVolumeTypeName == model.ProductVolumeTypeName).First().id;
                product.ProductVolume = model.ProductVolume;
                product.RetailPrice = model.RetailPrice;
                product.WholeSalePrice = model.WholeSalePrice;
                product.Quantity = model.Quantity;
            }
        }


        public void productOff()
        {
            if (OffValue <= product.Quantity)
            {
                product.Quantity -= OffValue;
                string json = JsonConvert.SerializeObject(product);
                int id = ProductsContent.IndexOf(ProductsContent.Where(x => x.Articul == product.Articul).First());
                ProductsFullData edited = ApiHelper.Put<List<ProductsFullData>>(json, "Products")[0];
                ProductsContent[id] = edited;
            } else
            {
                MessageBox.Show("Нельзя списать больше чем есть");
            }
            
        }

        public void productAdd()
        {
            product.Quantity += AddValue;
            string json = JsonConvert.SerializeObject(product);
            int id = ProductsContent.IndexOf(ProductsContent.Where(x => x.Articul == product.Articul).First());
            ProductsFullData edited = ApiHelper.Put<List<ProductsFullData>>(json, "Products")[0];
            ProductsContent[id] = edited;
        }





    }
}
