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
    internal class OrdersViewModel: BindingHelper
    {
        #region collections
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

        private ObservableCollection<PaymentMethodsModel> payContent;
        public ObservableCollection<PaymentMethodsModel> PayContent
        {
            get { return payContent; }
            set
            {
                payContent = value;
                OnPropertyChanged(nameof(PayContent));
            }
        }

        private ObservableCollection<OrdersStatusModel> statusContent;
        public ObservableCollection<OrdersStatusModel> StatusContent
        {
            get { return statusContent; }
            set
            {
                statusContent = value;
                OnPropertyChanged(nameof(StatusContent));
            }
        }

        #endregion
        #region newObj
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

        private PaymentMethodsModel method;
        public PaymentMethodsModel Method
        {
            get { return method; }
            set
            {
                method = value;
                OnPropertyChanged(nameof(Method));
            }
        }

        private OrdersStatusModel status;
        public OrdersStatusModel Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        #endregion

        private int selected = 0;

        public OrdersViewModel() 
        {
            OrdersContent = new ObservableCollection<OrdersFullData>(
                ApiHelper.Get<List<OrdersFullData>>("Orders").OrderBy(x => x.id));
            PayContent = new ObservableCollection<PaymentMethodsModel>(
                ApiHelper.Get<List<PaymentMethodsModel>>("PaymentMethods").OrderBy(x => x.id));
            StatusContent = new ObservableCollection<OrdersStatusModel>(
                ApiHelper.Get<List<OrdersStatusModel>>("OrdersStatus").OrderBy(x => x.id));
            Order = new OrdersModel();
            Method = new PaymentMethodsModel();
            Status = new OrdersStatusModel();
            Order.OrderDate = DateTime.Now.Date.ToString();
            selected = 0;
        }

        public void gridHandler(object sender, RoutedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid.SelectedItem != null)
            {
                if (grid.Name == "Methods")
                {
                    PaymentMethodsModel model = grid.SelectedItem as PaymentMethodsModel;
                    Method.PaymentMethodName = model.PaymentMethodName;
                    Method.id = model.id;
                    selected = model.id;
                } else if (grid.Name == "Statuses")
                {
                    OrdersStatusModel model = grid.SelectedItem as OrdersStatusModel;
                    Status.OrderStatusName = model.OrderStatusName;
                    Status.id = model.id;
                    selected = model.id;
                } else
                {
                    OrdersFullData model = grid.SelectedItem as OrdersFullData;
                    selected = model.id;
                    Order.id = model.id;
                    Order.OrderDescription = model.OrderDescription;
                    Order.CustomerID_id = model.CustomerID_id;
                    Order.PaymentMethodID_id = model.PaymentMethodID_id;
                    Order.OrderStatusID_id = model.OrderStatusID_id;
                    Order.TotalAmount = model.TotalAmount;
                    Order.ReceivedAmount = model.ReceivedAmount;
                    Order.OrderChange = model.OrderChange;
                    Order.OrderDate = model.OrderDate.ToString();
                }
            }
        }

        public void comboBoxHandler(object sender, RoutedEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx.SelectedItem != null)
            {
                if (cbx.Name == "Method") Order.PaymentMethodID_id = (cbx.SelectedItem as PaymentMethodsModel).id;
                else Order.OrderStatusID_id = (cbx.SelectedItem as OrdersStatusModel).id;
            }
        }


        public void addCommand(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name.Contains( "Pay"))
            {
                (bool isUnique, string error) = Check.IsUniq(PayContent, "PaymentMethodName", Method.PaymentMethodName);
                if (isUnique)
                {
                    string json = JsonConvert.SerializeObject(Method);
                    PaymentMethodsModel newObj = ApiHelper.Post<List<PaymentMethodsModel>>(json, "PaymentMethods")[0];
                    PayContent.Add(newObj);
                    PayContent = new ObservableCollection<PaymentMethodsModel>(PayContent.OrderBy(x => x.id));
                } else
                {
                    if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                    else MessageBox.Show("Значение названия должно быть уникальным");
                }
            } else if (btn.Name.Contains("Status"))
            {
                (bool isUnique, string error) = Check.IsUniq(StatusContent, "OrderStatusName", Status.OrderStatusName);
                if (isUnique)
                {
                    string json = JsonConvert.SerializeObject(Status);
                    OrdersStatusModel newObj = ApiHelper.Post<List<OrdersStatusModel>>(json, "OrdersStatus")[0];
                    StatusContent.Add(newObj);
                    StatusContent = new ObservableCollection<OrdersStatusModel>(StatusContent.OrderBy(x => x.id));
                }
                else
                {
                    if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                    else MessageBox.Show("Значение названия должно быть уникальным");
                }
            }
        }

        public void editCommand(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (selected != 0)
            {
                if (btn.Name.Contains("Pay"))
                {
                    (bool isUnique, string error) = Check.IsUniq(PayContent, "PaymentMethodName", Method.PaymentMethodName);
                    if (isUnique)
                    {
                        string json = JsonConvert.SerializeObject(Method);
                        int id = PayContent.IndexOf(PayContent.Where(x => x.id == Method.id).First());
                        PaymentMethodsModel edited = ApiHelper.Put<List<PaymentMethodsModel>>(json, "PaymentMethods")[0];
                        PayContent[id] = edited;
                        PayContent = new ObservableCollection<PaymentMethodsModel>(PayContent.OrderBy(x => x.id));
                    }
                    else
                    {
                        if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                        else MessageBox.Show("Значение названия должно быть уникальным");
                    }
                }
                else if (btn.Name.Contains("Status"))
                {
                    (bool isUnique, string error) = Check.IsUniq(StatusContent, "OrderStatusName", Status.OrderStatusName);
                    if (isUnique)
                    {
                        string json = JsonConvert.SerializeObject(Status);
                        int id = StatusContent.IndexOf(StatusContent.Where(x => x.id == Status.id).First());
                        OrdersStatusModel edited = ApiHelper.Put<List<OrdersStatusModel>>(json, "OrdersStatus")[0];
                        StatusContent[id] = edited;
                        StatusContent = new ObservableCollection<OrdersStatusModel>(StatusContent.OrderBy(x => x.id));
                    }
                    else
                    {
                        if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                        else MessageBox.Show("Значение названия должно быть уникальным");
                    }
                }
                else
                {
                    if (Order.ReceivedAmount >= Order.TotalAmount)
                    {
                        string json = JsonConvert.SerializeObject(Order);
                        int id = OrdersContent.IndexOf(OrdersContent.Where(x => x.id == Order.id).First());
                        OrdersFullData edited = ApiHelper.Put<List<OrdersFullData>>(json, "Orders")[0];
                        OrdersContent[id] = edited;
                        OrdersContent = new ObservableCollection<OrdersFullData>(OrdersContent.OrderBy(x => x.id));
                    } else
                    {
                        MessageBox.Show("Получено не может быть меньше чем стоимость заказа");
                    }
                }
            }
        }

        public void deleteCommand(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (selected != 0)
            {
                if (btn.Name.Contains("Pay"))
                {
                    bool canDelete = ApiHelper.Delete("PaymentMethods", id: selected);
                    if (canDelete)
                    {
                        PayContent.Remove(Method);
                        PayContent = new ObservableCollection<PaymentMethodsModel>(PayContent.OrderBy(x => x.id));
                        selected = 0;
                    }
                }
                else if (btn.Name.Contains("Status"))
                {
                    bool canDelete = ApiHelper.Delete("OrdersStatus", id: selected);
                    if (canDelete)
                    {
                        StatusContent.Remove(Status);
                        StatusContent = new ObservableCollection<OrdersStatusModel>(StatusContent.OrderBy(x => x.id));
                        selected = 0;
                    }
                }
            }

        }

    }
}
