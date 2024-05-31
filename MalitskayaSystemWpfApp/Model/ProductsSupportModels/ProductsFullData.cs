using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MalitskayaSystemWpfApp.Model.ProductsSupportModels
{
    internal class ProductsFullData: BindingHelper
    {
        public string Articul {get;set;}
        public string ProductImage { get;set;}
        public string BrandTypeName { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductName { get; set; } 
        public string ProductDescription { get; set;     }
        public string TextureTypeName { get; set; }
        public int ProductVolume { get; set; }
        public string ProductVolumeTypeName { get; set; }
        public int RetailPrice { get; set; }
        public int WholeSalePrice { get; set; }
        public int Quantity { get; set; }

    }
}
