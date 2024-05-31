using MalitskayaSystemWpfApp.Model;
using MalitskayaSystemWpfApp.Model.CustomersSupportModels;
using MalitskayaSystemWpfApp.Model.ProductsSupportModels;
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

namespace MalitskayaSystemWpfApp.ViewModel.CustomersViewModels
{
    internal class CustomersViewModel: BindingHelper
    {
        #region collections
        private ObservableCollection<CustomersFullData> customersContent;
        public ObservableCollection<CustomersFullData> CustomersContent
        {
            get { return customersContent; }
            set { 
                customersContent = value;
                OnPropertyChanged(nameof(CustomersContent));
            }
        }
        private ObservableCollection<ShippingTypesModel> shippingTypes;
        public ObservableCollection<ShippingTypesModel> ShippingTypes
        {
            get { return shippingTypes;}
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
        #region obj
        private CustomersModel customer;
        public CustomersModel Customer
        {
            get { return customer; }
            set
            {
                customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }
        #endregion

        #region commands
        public BindableCommands Add { get; set; }
        public BindableCommands Edit { get; set; }
        public BindableCommands Delete {  get; set; }
        #endregion


        public CustomersViewModel()
        {
            CustomersContent = new ObservableCollection<CustomersFullData>(
                ApiHelper.Get<List<CustomersFullData>>("Customers").OrderBy(x => x.id));
            ShippingTypes = ApiHelper.Get<ObservableCollection<ShippingTypesModel>>("ShippingTypes");
            Users = ApiHelper.Get<ObservableCollection<UsersModel>>("Users");
            Add = new BindableCommands(_ => addComand());
            Edit = new BindableCommands(_ => editCommand());
            Delete = new BindableCommands(_ => deleteCommand());
            Customer = new CustomersModel();
        }

        public void comboBoxHandler(object sender, RoutedEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx.SelectedItem != null)
            {
                if (cbx.Name == "shipping") Customer.ShippingTypeID_id = (cbx.SelectedItem as ShippingTypesModel).id;
                else Customer.UserID_id = (cbx.SelectedItem as UsersModel).UserID;
            }
        }

        public void gridHandler(object sender, RoutedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid.SelectedItem != null)
            {
                CustomersFullData model = grid.SelectedItem as CustomersFullData;
                Customer.id = model.id;
                Customer.FirstName = model.FirstName;
                Customer.LastName = model.LastName;
                Customer.Patronymic = model.Patronymic;
                Customer.Email = model.Email;
                Customer.Phone = model.Phone;
                Customer.City = model.City;
                Customer.CustomerAddress = model.CustomerAddress;
                Customer.isProfi = model.isProfi;
            }
        }

        public void addComand()
        {
            if (isCorrectCustomer())
            { 
                string json = JsonConvert.SerializeObject(Customer);
                CustomersFullData newCust = ApiHelper.Post<List<CustomersFullData>>(json, "Customers")[0];
                CustomersContent.Add(newCust);
                CustomersContent = new ObservableCollection<CustomersFullData>(
                    ApiHelper.Get<ObservableCollection<CustomersFullData>>("Customers").OrderBy(x => x.id));
            }
        }

        public void editCommand()
        {
            if (Customer.id != 0)
            {
                if (isCorrectCustomer())
                {
                    string json = JsonConvert.SerializeObject(Customer);
                    int id = CustomersContent.IndexOf(CustomersContent.Where(x => x.id == Customer.id).First());
                    CustomersFullData edit = ApiHelper.Put<List<CustomersFullData>>(json, "Customers")[0];
                    CustomersContent[id] = edit;
                    CustomersContent = new ObservableCollection<CustomersFullData>(
                    ApiHelper.Get<ObservableCollection<CustomersFullData>>("Customers").OrderBy(x => x.id));
                }
            }
        }

        public void deleteCommand()
        {
            if (Customer.id != 0)
            {
                bool canDelete = ApiHelper.Delete("Customers", id: Customer.id);
                if (canDelete)
                {
                    CustomersContent.Remove(CustomersContent.Where(x => x.id == Customer.id).First());
                    CustomersContent = new ObservableCollection<CustomersFullData>(
                    ApiHelper.Get<ObservableCollection<CustomersFullData>>("Customers").OrderBy(x => x.id));
                    Customer.id = 0;
                }
            } else
            {
                MessageBox.Show("Сначала нужно выбрать покупателя");
            }

        }

        private bool isCorrectCustomer()
        {
            if (!Check.IsLetters(Customer.FirstName))
            {
                MessageBox.Show("Имя должно состоять только из букв русского и английского алфавита");
            }
            else if (!Check.IsLetters(Customer.LastName))
            {
                MessageBox.Show("Имя должно состоять только из букв русского и английского алфавита");
            }
            else if (!Check.IsValidEmail(Customer.Email))
            {
                MessageBox.Show("Почта указана не корректно");
            }
            else if (!Check.IsPhone(Customer.Phone))
            {
                MessageBox.Show("Телефон указан не корректно");
            }
            else if (!Check.IsUniq<CustomersFullData>(CustomersContent, "Phone", Customer.Phone).Item1)
            {
                MessageBox.Show("Телефон не является уникальным");
            }
            else if (!Check.IsValidAddress(Customer.CustomerAddress))
            {
                MessageBox.Show("Адрес указан не корректно");
            }
            else if (!Check.IsLetters(Customer.City))
            {
                MessageBox.Show("Город указан не корректно");
            }
            else if (Customer.ShippingTypeID_id == 0)
            {
                MessageBox.Show("Не выбран метод доставки");
            } else 
            {
                
                return true;
            }
            return true; ;
        }

    }
}
