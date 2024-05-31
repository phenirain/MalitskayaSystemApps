using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model.OrdersSupportModels
{
    internal class ShippingsFullData
    {
        public int id { get; set; }
        public int OrderID_id { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string CustomerAddress { get; set; }
        public string City { get; set; }
        public string ShippingTypeName { get; set; }
        public float ShippingPrice { get; set; }
        public string PaymentMethodName { get; set; }
        public string ShippingDate { get; set; }
        public TimeSpan ShippingGoodBeginTime { get; set; }
        public TimeSpan ShippingGoodEndTime { get; set; }
    }
}
