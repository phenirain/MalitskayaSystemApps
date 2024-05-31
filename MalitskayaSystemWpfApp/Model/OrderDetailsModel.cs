using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model
{
    internal class OrderDetailsModel: BindingHelper
    {
        public int id { get; set; }
        private int orderid;
        public int OrderID_id { get { return orderid; } set { orderid = value; OnPropertyChanged(); } }
        private string articul;
        public string Articul_id { get { return articul; } set { articul = value; OnPropertyChanged(); } }
        private int quantity;
        public int Quantity { get { return quantity; } set { quantity = value; OnPropertyChanged(); } }
        private int amount;
        public int Amount { get { return amount; } set {  amount = value; OnPropertyChanged(); } }
    }
}
