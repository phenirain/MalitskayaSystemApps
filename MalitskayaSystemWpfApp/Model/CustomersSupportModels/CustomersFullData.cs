using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model.CustomersSupportModels
{
    internal class CustomersFullData
    {
        public int id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Phone {  get; set; }
        public string CustomerAddress { get; set; }
        public string City { get; set; }
        public bool isProfi {  get; set; }
        public string ShippingTypeName { get; set; }
        public string UserName { get; set; }
        public int? UserID_id { get; set; }
        public string ShippingTypeID_id { get; set; }
    }
}
