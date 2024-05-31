using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model.OrdersSupportModels
{
    internal class OrderDetailsFullData
    {
        public int id { get; set; }
        public int OrderID_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool isProfi { get; set; }
        public string Articul {  get; set; }
        public string ProductName { get; set; }
        public int RetailPrice { get; set; }
        public int WholeSalePrice { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
    }
}
