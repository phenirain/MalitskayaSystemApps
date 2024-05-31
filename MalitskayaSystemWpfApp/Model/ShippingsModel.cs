using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model
{
    internal class ShippingsModel: BindingHelper
    {
        public int id {  get; set; }
        public int OrderID_id { get; set; }
        private float shippingPrice { get; set; }
        public float ShippingPrice { get { return shippingPrice; } set { shippingPrice = value; OnPropertyChanged(); } }
        private string shippingDate { get; set; }
        public string ShippingDate { get { return shippingDate; } set { shippingDate = value; OnPropertyChanged(); } }
        private string shippingTime { get; set; }
        public string ShippingGoodBeginTime { get { return shippingTime; } set { shippingTime = value; OnPropertyChanged(); } }
        private string shippingEnd { get; set; }
        public string ShippingGoodEndTime { get { return shippingEnd; } set { shippingEnd = value; OnPropertyChanged(); } }
    }
}
