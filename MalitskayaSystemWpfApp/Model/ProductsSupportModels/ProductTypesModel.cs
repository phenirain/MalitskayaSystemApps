using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model
{
    internal class ProductTypesModel: BindingHelper
    {
        public int id {  get; set; }
        private string product;
        public string ProductTypeName { get { return product; } set { product = value; OnPropertyChanged(); } }
    }
}
