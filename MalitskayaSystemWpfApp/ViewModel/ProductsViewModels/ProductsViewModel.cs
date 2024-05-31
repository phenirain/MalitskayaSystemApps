using MalitskayaSystemWpfApp.Model;
using MalitskayaSystemWpfApp.Model.OrdersSupportModels;
using MalitskayaSystemWpfApp.Model.ProductsSupportModels;
using MalitskayaSystemWpfApp.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;
using System.IO;

namespace MalitskayaSystemWpfApp.ViewModel.ProductsViewModels
{
    internal class ProductsViewModel: BindingHelper
    {
        #region collections
        private ObservableCollection<ProductsFullData> productsContent;
        public ObservableCollection<ProductsFullData> ProductsContent
        {
            get { return productsContent; }
            set
            {
                productsContent = value;
                OnPropertyChanged(nameof(ProductsContent));
            }
        }

        private ObservableCollection<BrandTypesModel> brandTypes;
        public ObservableCollection<BrandTypesModel> BrandTypes
        {
            get { return brandTypes; }
            set
            {
                brandTypes = value;
                OnPropertyChanged(nameof(BrandTypes));
            }
        }

        private ObservableCollection<ProductTypesModel> productTypes;
        public ObservableCollection<ProductTypesModel> ProductTypes
        {
            get { return productTypes; }
            set
            {
                productTypes = value;
                OnPropertyChanged(nameof(ProductTypes));
            }
        }
        private ObservableCollection<ProductVolumeTypesModel> productVolumeTypes;
        public ObservableCollection<ProductVolumeTypesModel> ProductVolumeTypes
        {
            get { return productVolumeTypes; }
            set
            {
                productVolumeTypes = value;
                OnPropertyChanged(nameof(ProductVolumeTypes));
            }
        }
        private ObservableCollection<TextureTypesModel> textureTypes;
        public ObservableCollection<TextureTypesModel> TextureTypes
        {
            get { return textureTypes; }
            set
            {
                textureTypes = value;
                OnPropertyChanged(nameof(TextureTypes));
            }
        }

        #endregion
        #region newObj
        private ProductsModel product;
        public ProductsModel Product
        {
            get { return product; } 
            set
            {
                product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        private BrandTypesModel brandType;
        public BrandTypesModel BrandType
        {
            get { return brandType; }
            set
            {
                brandType = value;
                OnPropertyChanged(nameof(BrandType));
            }
        }
        public ProductTypesModel productType;
        private ProductTypesModel ProductType
        {
            get { return productType; }
            set
            {
                productType = value;
                OnPropertyChanged(nameof(ProductType));
            }
        }
        public ProductVolumeTypesModel volumeType;
        private ProductVolumeTypesModel VolumeType
        {
            get { return volumeType; }
            set
            {
                volumeType = value;
                OnPropertyChanged(nameof(VolumeType));
            }
        }
        public TextureTypesModel textureType;
        private TextureTypesModel TextureType
        {
            get { return textureType; }
            set
            {
                textureType = value;
                OnPropertyChanged(nameof(TextureType));
            }
        }
        #endregion
        private string typeP;
        public string TypeProduct
        {
            get { return ProductType.ProductTypeName; }
            set { ProductType.ProductTypeName= value; OnPropertyChanged(nameof(TypeProduct));}
        }

        public string TypeVolume
        {
            get { return VolumeType.ProductVolumeTypeName; }
            set {  VolumeType.ProductVolumeTypeName = value; OnPropertyChanged(); }
        }

        public string TypeTexture
        {
            get { return TextureType.TextureTypeName; }
            set { TextureType.TextureTypeName = value; OnPropertyChanged(); }
        }

        private string imagename;
        public string ImageName
        {
            get => imagename;
            set { imagename = value; OnPropertyChanged(); }
        }


        private int selected;
        private string selectedProduct;

        public ProductsViewModel()
        {
            ProductsContent = new ObservableCollection<ProductsFullData>(
                ApiHelper.Get<List<ProductsFullData>>("Products").OrderBy(x => x.Articul));
            BrandTypes = new ObservableCollection<BrandTypesModel>(
                ApiHelper.Get<List<BrandTypesModel>>("BrandTypes").OrderBy(x => x.id));
            ProductTypes = new ObservableCollection<ProductTypesModel>(
                ApiHelper.Get<List<ProductTypesModel>>("ProductTypes").OrderBy(x => x.id));
            TextureTypes = new ObservableCollection<TextureTypesModel>(
                ApiHelper.Get<List<TextureTypesModel>>("TextureTypes").OrderBy(x => x.id));
            ProductVolumeTypes = new ObservableCollection<ProductVolumeTypesModel>(
                ApiHelper.Get<List<ProductVolumeTypesModel>>("ProductVolumeTypes").OrderBy(x => x.id));
            Product = new ProductsModel();
            BrandType = new BrandTypesModel();
            TextureType = new TextureTypesModel();
            VolumeType = new ProductVolumeTypesModel();
            ProductType = new ProductTypesModel();
        }


        public void gridHandler(object sender, RoutedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid.SelectedItem != null)
            {
                if (grid.Name == "brands")
                {
                    BrandTypesModel model = grid.SelectedItem as BrandTypesModel;
                    BrandType.BrandTypeName = model.BrandTypeName;
                    BrandType.id = model.id;
                    selected = model.id;
                }
                else if (grid.Name == "types")
                {
                    ProductTypesModel model = grid.SelectedItem as ProductTypesModel;
                    TypeProduct = model.ProductTypeName;
                    ProductType.id = model.id;
                    selected = model.id;
                }
                else if (grid.Name == "textures")
                {
                    TextureTypesModel model = grid.SelectedItem as TextureTypesModel;
                    TypeTexture = model.TextureTypeName;
                    TextureType.id = model.id;
                    selected = model.id;
                }
                else if (grid.Name == "volumes")
                {
                    ProductVolumeTypesModel model = grid.SelectedItem as ProductVolumeTypesModel;
                    TypeVolume = model .ProductVolumeTypeName;
                    VolumeType.id = model.id;
                    selected = model.id;
                }
                else
                {
                    ProductsFullData model = grid.SelectedItem as ProductsFullData;
                    selectedProduct = model.Articul;
                    Product.Articul = model.Articul;
                    if (BrandTypes.Where(x => x.BrandTypeName == model.BrandTypeName).Any())
                        Product.BrandTypeID_id = BrandTypes.Where(x => x.BrandTypeName == model.BrandTypeName).First().id;
                    if (ProductTypes.Where(x => x.ProductTypeName == model.ProductTypeName).Any())
                        Product.ProductTypeID_id = ProductTypes.Where(x => x.ProductTypeName == model.ProductTypeName).First().id;
                    Product.ProductName = model.ProductName;
                    Product.ProductDescription = model.ProductDescription;
                    if (TextureTypes.Where(x => x.TextureTypeName == model.TextureTypeName).Any())
                        Product.TextureTypeID_id = TextureTypes.Where(x => x.TextureTypeName == model.TextureTypeName).First().id;
                    Product.ProductVolume = model.ProductVolume;
                    if (ProductVolumeTypes.Where(x => x.ProductVolumeTypeName == model.ProductVolumeTypeName).Any())
                        Product.ProductVolumeTypeID_id = ProductVolumeTypes.Where(x => x.ProductVolumeTypeName == model.ProductVolumeTypeName).First().id;
                    Product.RetailPrice = model.RetailPrice;
                    Product.WholeSalePrice = model.WholeSalePrice;
                    Product.Quantity = model.Quantity;
                }
            }
        }

        public void comboBoxHandler(object sender, RoutedEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx.SelectedItem != null)
            {
                if (cbx.Name == "brand") Product.BrandTypeID_id =  (cbx.SelectedItem as BrandTypesModel).id;
                else if (cbx.Name == "type") Product.ProductTypeID_id = (cbx.SelectedItem as  ProductTypesModel).id;
                else if (cbx.Name == "texture") Product.TextureTypeID_id = (cbx.SelectedItem as  TextureTypesModel).id;
                else Product.ProductVolumeTypeID_id = (cbx.SelectedItem as ProductVolumeTypesModel).id;
            }
        }



        public void addCommand(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name.Contains("Brand"))
            {
                (bool isUnique, string error) = Check.IsUniq(BrandTypes, "BrandTypeName", BrandType.BrandTypeName);
                if (isUnique)
                {
                    string json = JsonConvert.SerializeObject(BrandType);
                    BrandTypesModel newObj = ApiHelper.Post<List<BrandTypesModel>>(json, "BrandTypes")[0];
                    BrandTypes.Add(newObj);
                    BrandTypes = new ObservableCollection<BrandTypesModel>(BrandTypes.OrderBy(x => x.id));
                }
                else
                {
                    if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                    else MessageBox.Show("Значение названия должно быть уникальным");
                }
            }
            else if (btn.Name.Contains("Type"))
            {
                (bool isUnique, string error) = Check.IsUniq(ProductTypes, "ProductTypeName", ProductType.ProductTypeName);
                if (isUnique)
                {
                    string json = JsonConvert.SerializeObject(ProductType);
                    ProductTypesModel newObj = ApiHelper.Post<List<ProductTypesModel>>(json, "ProductTypes")[0];
                    ProductTypes.Add(newObj);
                    ProductTypes = new ObservableCollection<ProductTypesModel>(ProductTypes.OrderBy(x => x.id));
                }
                else
                {
                    if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                    else MessageBox.Show("Значение названия должно быть уникальным");
                }
            }
            else if (btn.Name.Contains("Texture"))
            {
                (bool isUnique, string error) = Check.IsUniq(TextureTypes, "TextureTypeName", TextureType.TextureTypeName);
                if (isUnique)
                {
                    string json = JsonConvert.SerializeObject(TextureType);
                    TextureTypesModel newObj = ApiHelper.Post<List<TextureTypesModel>>(json, "TextureTypes")[0];
                    TextureTypes.Add(newObj);
                    TextureTypes = new ObservableCollection<TextureTypesModel>(TextureTypes.OrderBy(x => x.id));
                }
                else
                {
                    if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                    else MessageBox.Show("Значение названия должно быть уникальным");
                }
            }
            else if (btn.Name.Contains("Volume"))
            {
                (bool isUnique, string error) = Check.IsUniq(ProductVolumeTypes, "ProductVolumeTypeName", VolumeType.ProductVolumeTypeName);
                if (isUnique)
                {
                    string json = JsonConvert.SerializeObject(VolumeType);
                    ProductVolumeTypesModel newObj = ApiHelper.Post<List<ProductVolumeTypesModel>>(json, "ProductVolumeTypes")[0];
                    ProductVolumeTypes.Add(newObj);
                    ProductVolumeTypes = new ObservableCollection<ProductVolumeTypesModel>(ProductVolumeTypes.OrderBy(x => x.id));
                }
                else
                {
                    if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                    else MessageBox.Show("Значение названия должно быть уникальным");
                }
            }
            else
            {
                if (isCorrectProduct("add"))
                {
                    string json = JsonConvert.SerializeObject(Product);
                    ProductsFullData newObj = ApiHelper.Post<List<ProductsFullData>>(json, "Products")[0];
                    ProductsContent.Add(newObj);
                    ProductsContent = new ObservableCollection<ProductsFullData>(ProductsContent.OrderBy(x => x.Articul));
                }
            }
        }

        public void editCommand(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (selected != 0 || Product.Articul != null)
            {
                if (btn.Name.Contains("Brand"))
                {
                    (bool isUnique, string error) = Check.IsUniq(BrandTypes, "BrandTypeName", BrandType.BrandTypeName);
                    if (isUnique)
                    {
                        string json = JsonConvert.SerializeObject(BrandType);
                        int id = BrandTypes.IndexOf(BrandTypes.Where(x => x.id == BrandType.id).First());
                        BrandTypesModel edited = ApiHelper.Put<List<BrandTypesModel>>(json, "BrandTypes")[0];
                        BrandTypes[id] = edited;
                        BrandTypes = new ObservableCollection<BrandTypesModel>(BrandTypes.OrderBy(x => x.id));
                    }
                    else
                    {
                        if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                        else MessageBox.Show("Значение названия должно быть уникальным");
                    }
                }
                else if (btn.Name.Contains("Type"))
                {
                    (bool isUnique, string error) = Check.IsUniq(ProductTypes, "ProductTypeName", ProductType.ProductTypeName);
                    if (isUnique)
                    {
                        string json = JsonConvert.SerializeObject(ProductType);
                        int id = ProductTypes.IndexOf(ProductTypes.Where(x => x.id == ProductType.id).First());
                        ProductTypesModel edited = ApiHelper.Put<List<ProductTypesModel>>(json, "ProductTypes")[0];
                        ProductTypes[id] = edited;
                        ProductTypes = new ObservableCollection<ProductTypesModel>(ProductTypes.OrderBy(x => x.id));
                    }
                    else
                    {
                        if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                        else MessageBox.Show("Значение названия должно быть уникальным");
                    }
                }
                else if (btn.Name.Contains("Texture"))
                {
                    (bool isUnique, string error) = Check.IsUniq(TextureTypes, "TextureTypeName", TextureType.TextureTypeName);
                    if (isUnique)
                    {
                        string json = JsonConvert.SerializeObject(TextureType);
                        int id = TextureTypes.IndexOf(TextureTypes.Where(x => x.id == TextureType.id).First());
                        TextureTypesModel edited = ApiHelper.Put<List<TextureTypesModel>>(json, "TextureTypes")[0];
                        TextureTypes[id] = edited;
                        TextureTypes = new ObservableCollection<TextureTypesModel>(TextureTypes.OrderBy(x => x.id));
                    }
                    else
                    {
                        if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                        else MessageBox.Show("Значение названия должно быть уникальным");
                    }
                   
                }
                else if (btn.Name.Contains("Volume"))
                {
                    (bool isUnique, string error) = Check.IsUniq(ProductVolumeTypes, "ProductVolumeTypeName", VolumeType.ProductVolumeTypeName);
                    if (isUnique)
                    {
                        string json = JsonConvert.SerializeObject(VolumeType);
                        int id = ProductVolumeTypes.IndexOf(ProductVolumeTypes.Where(x => x.id == VolumeType.id).First());
                        ProductVolumeTypesModel edited = ApiHelper.Put<List<ProductVolumeTypesModel>>(json, "ProductVolumeTypes")[0];
                        ProductVolumeTypes[id] = edited;
                        ProductVolumeTypes = new ObservableCollection<ProductVolumeTypesModel>(ProductVolumeTypes.OrderBy(x => x.id));
                    }
                    else
                    {
                        if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                        else MessageBox.Show("Значение названия должно быть уникальным");
                    }
                }
                else
                {
                    if (isCorrectProduct("edit"))
                    {
                        string json = JsonConvert.SerializeObject(Product);
                        int id = ProductsContent.IndexOf(ProductsContent.Where(x => x.Articul == selectedProduct).First());
                        ProductsFullData edited = ApiHelper.Put<List<ProductsFullData>>(json, "Products")[0];
                        ProductsContent[id] = edited;
                        ProductsContent = new ObservableCollection<ProductsFullData>(ProductsContent.OrderBy(x => x.Articul));
                    }
                }
            } else
            {
                MessageBox.Show("Сначала нужно выбрать запись");
            }
        }

        public void deleteCommand(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (selected != 0 || Product.Articul != null)
            {
                if (btn.Name.Contains("Brand"))
                {
                    bool canDelete = ApiHelper.Delete("BrandTypes", id: selected);
                    if (canDelete)
                    {
                        BrandTypes.Remove(BrandType);
                        BrandTypes = new ObservableCollection<BrandTypesModel>(BrandTypes.OrderBy(x => x.id));
                        selected = 0;
                    } else
                    {
                        MessageBox.Show("Данная запись используется в других таблицах(");
                    }
                }
                else if (btn.Name.Contains("Type"))
                {
                    bool canDelete = ApiHelper.Delete("ProductTypes", id: selected);
                    if (canDelete)
                    {
                        ProductTypes.Remove(ProductType);
                        ProductTypes = new ObservableCollection<ProductTypesModel>(ProductTypes.OrderBy(x => x.id));
                        selected = 0;
                    }
                    else
                    {
                        MessageBox.Show("Данная запись используется в других таблицах(");
                    }
                }
                else if (btn.Name.Contains("Texture"))
                {
                    bool canDelete = ApiHelper.Delete("TextureTypes", id: selected);
                    if (canDelete)
                    {
                        TextureTypes.Remove(TextureType);
                        TextureTypes = new ObservableCollection<TextureTypesModel>(TextureTypes.OrderBy(x => x.id));
                        selected = 0;
                    }
                    else
                    {
                        MessageBox.Show("Данная запись используется в других таблицах(");
                    }
                }
                else if (btn.Name.Contains("Volume"))
                {
                    bool canDelete = ApiHelper.Delete("ProductVolumeTypes", id: selected);
                    if (canDelete)
                    {
                        ProductVolumeTypes.Remove(VolumeType);
                        ProductVolumeTypes = new ObservableCollection<ProductVolumeTypesModel>(ProductVolumeTypes.OrderBy(x => x.id));
                        selected = 0;
                    }
                    else
                    {
                        MessageBox.Show("Данная запись используется в других таблицах(");
                    }
                }
                else
                {
                    bool canDelete = ApiHelper.Delete("Products", articul: Product.Articul);
                    if (canDelete)
                    {
                        ProductsFullData toDelete = ProductsContent.Where(x => x.Articul == Product.Articul).FirstOrDefault();
                        if (toDelete != null)
                        {
                            ProductsContent.Remove(toDelete);
                            ProductsContent = new ObservableCollection<ProductsFullData>(ProductsContent.OrderBy(x => x.Articul));
                            selected = 0;
                        } else
                        {
                            MessageBox.Show("Такого артикула нет");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данная запись используется в других таблицах(");
                    }
                }
            }
        }

        private bool isCorrectProduct(string action)
        {
            if (action == "add")
            {
                if (!Check.IsUniq(ProductsContent, "Articul", Product.Articul).Item1)
                {

                    MessageBox.Show("Артикул должен быть уникальным");
                    return false;
                }
            }  else if (action == "edit")
            {
                if (Product.Articul == null && Product.Articul == "")
                {
                    MessageBox.Show("Артикул не может быть пустым");
                    return false;
                }
            }
            else if (string.IsNullOrEmpty(Product.ProductName))
            {
                MessageBox.Show("Название не может быть пустым");
                return false;
            } else if (Product.BrandTypeID_id == 0)
            {
                MessageBox.Show("Бренд не может быть не выбран");
                return false;
            } else if (Product.ProductVolume <= 0) 
            {
                MessageBox.Show("Объем должен быть больше 0");
            } 
            else if (Product.ProductVolume <= 0 && Product.ProductVolumeTypeID_id == 0)
            {
                MessageBox.Show("Нужно выбрать тип объема");
            } else if (Product.RetailPrice <= 0) 
            {
                MessageBox.Show("Розничная цена должна быть больше 0");
            } else if (Product.WholeSalePrice <= 0)
            {
                MessageBox.Show("Опт цена должна быть больше 0");
            } else if (Product.Quantity <= 0)
            {
                MessageBox.Show("Количество должно быть больше 0");
            }
            return true;
        }
    }
}
