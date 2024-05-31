using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model
{
    internal class ProductVolumeTypesModel: BindingHelper
    {
        public int id { get; set; }
        private string volume;
        public string ProductVolumeTypeName { get { return volume; } set { volume = value; OnPropertyChanged(); } }
    }
}
