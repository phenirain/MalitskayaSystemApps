using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model
{
    internal class ProductsModel: BindingHelper
    {
        private string articul;
        public string Articul { get { return articul; } set { articul = value; OnPropertyChanged(); } }
        public string ProductImage { get; set; }

        public int? BrandTypeID_id { get; set; }
        public int? ProductTypeID_id { get; set; }
        private string name;
        public string ProductName { get { return name; } set { name = value; OnPropertyChanged(); } }
        private string description;
        public string ProductDescription { get { return description; } set { description = value; OnPropertyChanged(); } }
        public int? TextureTypeID_id { get; set; }
        private int volume;
        public int ProductVolume { get { return volume; } set { volume = value; OnPropertyChanged(); } }
        public int? ProductVolumeTypeID_id { get; set; }
        private int retailprice;
        public int RetailPrice { get { return retailprice; } set { retailprice = value; OnPropertyChanged(); } }
        private int wholesaleprice;
        public int WholeSalePrice { get { return wholesaleprice; } set { wholesaleprice = value; OnPropertyChanged(); } }
        private int quantity;
        public int Quantity { get { return quantity; } set { quantity = value; OnPropertyChanged(); } }
    }
}
