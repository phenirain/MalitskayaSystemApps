using MalitskayaSystemWpfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model
{
    internal class TextureTypesModel: BindingHelper
    {
        public int id { get; set; }
        private string texture;
        public string TextureTypeName { get { return texture; } set { texture = value; OnPropertyChanged(); } }
    }
}
