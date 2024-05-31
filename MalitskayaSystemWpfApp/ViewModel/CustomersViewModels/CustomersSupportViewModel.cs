using MalitskayaSystemWpfApp.Model;
using MalitskayaSystemWpfApp.Model.CustomersSupportModels;
using MalitskayaSystemWpfApp.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MalitskayaSystemWpfApp.ViewModel.CustomersViewModels
{
    internal class CustomersSupportViewModel: BindingHelper
    {
        #region collection
        private ObservableCollection<ShippingTypesModel> shippingTypes;
        public ObservableCollection<ShippingTypesModel> ShippingTypes
        {
            get { return shippingTypes; }
            set
            {
                shippingTypes = value;
                OnPropertyChanged(nameof(ShippingTypes));
            }
        }

        private ObservableCollection<UsersModel> users;
        public ObservableCollection<UsersModel> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        #endregion
        #region newObjects
        private ShippingTypesModel shippingType;
        public ShippingTypesModel ShippingType
        {
            get { return shippingType; }
            set
            {
                shippingType = value;
                OnPropertyChanged(nameof(ShippingType));
            }
        }
        #endregion

        public BindableCommands Add { get; set; }
        public BindableCommands Edit { get; set; }
        public BindableCommands Delete { get; set; }

        public CustomersSupportViewModel()
        {
            ShippingTypes = new ObservableCollection<ShippingTypesModel>(
                ApiHelper.Get<List<ShippingTypesModel>>("ShippingTypes").OrderBy(x => x.id));
            Users = ApiHelper.Get<ObservableCollection<UsersModel>>("Users");
            ShippingType = new ShippingTypesModel();
            Add = new BindableCommands(_ => addComand());
            Edit = new BindableCommands(_ => editCommand());
            Delete = new BindableCommands(_ => deleteCommand());
        }

        public void gridHandler(object sender, RoutedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid.SelectedItem != null)
            {
                ShippingTypesModel model = grid.SelectedItem as ShippingTypesModel;
                ShippingType.id = model.id;
                ShippingType.ShippingTypeName = model.ShippingTypeName;
            }
        }

        public void addComand()
        {
            (bool isUnique, string error) = Check.IsUniq<ShippingTypesModel>(
                set: ShippingTypes, column: "ShippingTypeName", value: ShippingType.ShippingTypeName);
            if (isUnique)
            {
                string json = JsonConvert.SerializeObject(ShippingType);
                ShippingTypesModel newObj = ApiHelper.Post<List<ShippingTypesModel>>(json, "ShippingTypes")[0];
                ShippingTypes.Add(newObj);
                ShippingTypes = new ObservableCollection<ShippingTypesModel>(ShippingTypes.OrderBy(x => x.id));
            } else
            {
                if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                else MessageBox.Show("Значение названиея должно быть уникальным");
            }
        }

        public void editCommand()
        {
            if (ShippingType.id != 0)
            { 
                (bool isUnique, string error) = Check.IsUniq<ShippingTypesModel>(
                    set: ShippingTypes, column: "ShippingTypeName", value: ShippingType.ShippingTypeName);
                if (isUnique)
                {
                    string json = JsonConvert.SerializeObject(ShippingType);
                    int id = ShippingTypes.IndexOf(ShippingTypes.Where(x => x.id == ShippingType.id).First());
                    ShippingTypesModel edited = ApiHelper.Put<List<ShippingTypesModel>>(json, "ShippingTypes")[0];
                    ShippingTypes[id] = edited;
                    ShippingTypes = new ObservableCollection<ShippingTypesModel>(ShippingTypes.OrderBy(x => x.id));
                } else
                {
                    if (error == "empty") MessageBox.Show("Значение названия не может быть пустым");
                    else MessageBox.Show("Значение названия должно быть уникальным");
                    MessageBox.Show("Сначало нужно выбрать тип");
                }
            } else
            {
                MessageBox.Show("Сначало нужно выбрать тип");
            }
        }

        public void deleteCommand()
        {
            if (ShippingType.id != 0)
            {
                bool canDelete = ApiHelper.Delete("ShippingTypes", id: ShippingType.id);
                if (canDelete)
                {
                    ShippingTypes.Remove(ShippingType);
                    ShippingTypes = new ObservableCollection<ShippingTypesModel>(ShippingTypes.OrderBy(x => x.id));
                    ShippingType.id = 0;
                }
            } else
            {
                MessageBox.Show("Сначала нужно выбрать тип");
            }

        }

    }
}
