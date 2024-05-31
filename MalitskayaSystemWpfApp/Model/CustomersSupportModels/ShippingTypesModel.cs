using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model.CustomersSupportModels
{
    internal class ShippingTypesModel: BindingHelper
    {
        public int id {  get; set; }
        private string shippingtypename;
        public string ShippingTypeName
        {
            get { return shippingtypename; } set { shippingtypename = value; OnPropertyChanged(); } }
    }
}
