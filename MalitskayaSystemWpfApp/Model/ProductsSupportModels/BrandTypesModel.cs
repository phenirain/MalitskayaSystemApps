using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MalitskayaSystemWpfApp.Model
{
    internal class BrandTypesModel: BindingHelper
    {
        public int id { get; set; }

        private string brand;
        public string BrandTypeName { get { return  brand; } set {  brand = value; OnPropertyChanged(); } }
    }
}
