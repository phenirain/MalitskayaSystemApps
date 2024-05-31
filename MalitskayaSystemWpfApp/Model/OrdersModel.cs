using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model
{
    internal class OrdersModel: BindingHelper
    {
        public int id {  get; set; }
        private string description;
        public string OrderDescription { get { return description; } set { description = value; OnPropertyChanged(); } }
        public int CustomerID_id { get; set; }
        public int PaymentMethodID_id { get; set; }
        private int total;
        public int TotalAmount { get { return total; } set { total = value; OnPropertyChanged(); } }
        private int received;
        public int ReceivedAmount { get { return received; } set { received = value; OnPropertyChanged(); } }
        private int change;
        public int OrderChange { get { return change; } set { change = value; OnPropertyChanged(); } }
        public int OrderStatusID_id { get; set; }
        private string orderdate;
        public string OrderDate {  get { return orderdate; } set {  orderdate = value; OnPropertyChanged(); } }
    }
}
