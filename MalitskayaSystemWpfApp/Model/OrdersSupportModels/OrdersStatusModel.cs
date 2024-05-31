using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model.OrdersSupportModels
{
    internal class OrdersStatusModel: BindingHelper
    {
        public int id {  get; set; }
        private string ordersstatusname;
        public string OrderStatusName
        {
            get { return ordersstatusname; } set {  ordersstatusname = value; OnPropertyChanged(); }
        }
    }
}
