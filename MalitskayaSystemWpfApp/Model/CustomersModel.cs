using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model
{
    internal class CustomersModel: BindingHelper
    {
        public int id { get; set; }
        private string firstname;
        public string FirstName {
            get { return firstname; } set {  firstname = value;  OnPropertyChanged(); }
        }
        private string lastname;
        public string LastName { get { return lastname; } set { lastname = value; OnPropertyChanged(); } }
        private string patronymic;
        public string Patronymic { get { return patronymic; } set { patronymic= value; OnPropertyChanged(); } }
        private string email;
        public string Email { get { return email; } set { email = value; OnPropertyChanged(); } }
        private string phone;
        public string Phone { get { return phone; } set { phone = value; OnPropertyChanged(); } }
        private string customeraddress;
        public string CustomerAddress { get { return customeraddress; } set { customeraddress = value; OnPropertyChanged(); } }
        private string city;
        public string City { get { return city; } set { city = value; OnPropertyChanged(); } }
        private bool isprofi;
        public bool isProfi { get { return isprofi; } set { isprofi= value; OnPropertyChanged(); } }
        private int shippingtypeid;
        public int ShippingTypeID_id { get { return shippingtypeid; } set { shippingtypeid = value; OnPropertyChanged(); } }
        public int? UserID_id { get; set; }
    }
}
