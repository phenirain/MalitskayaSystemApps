using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model.OrdersSupportModels
{
    internal class OrdersFullData
    {
        public int id {  get; set; }
        public string OrderDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool isProfi { get; set; }
        public string City { get; set; }
        public string ShippingTypeName { get; set; }
        public string PaymentMethodName { get; set; }
        public int TotalAmount { get; set; }
        public int ReceivedAmount { get; set; }
        public int OrderChange {  get; set; }
        public string OrderDate { get; set; }
        public string OrderStatusName { get; set; }

        public int CustomerID_id { get ; set; }
        public int PaymentMethodID_id { get; set; }
        public int OrderStatusID_id {  get; set; }
    }
}
