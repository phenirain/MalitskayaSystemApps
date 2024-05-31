using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model.OrdersSupportModels
{
    internal class PaymentMethodsModel:BindingHelper
    {
        public int id {  get; set; }
        private string method;
        public string  PaymentMethodName { get { return method; } set { method = value;OnPropertyChanged(); } }
    }
}
