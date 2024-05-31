using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MalitskayaSystemWpfApp.Utilities
{
    internal class Check
    {

        static public (bool, string) IsUniq<T>(ObservableCollection<T> set, string column, string value)
        {
            if (value != "" && value != null)
            {
                var property = typeof(T).GetProperty(column);
                foreach (T item in set)
                {
                    if ((string)property.GetValue(item) == value) return (false, "notUNIQ");
                }
                return (true, "");
            } else
            {
                return (false, "empty");
            }
        }

        static public bool IsLetters(string value)
        {
            Regex regex = new Regex(@"^[a-zA-Zа-яА-Я\s.]+$");
            return regex.IsMatch(value);
        }

        static public bool IsValidEmail(string email)
        {
            string pattern = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            // w+ - letters or digits or lowerhandwriting
            // ([-+.]\w)* - one or many w+ begin with -+.
            // @ - divider
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        static public bool IsPhone(string phone)
        {
            if (phone.Length == 11) return true;
            else return false;
        }


        static public bool IsValidAddress(string address)
        {
            string pattern = @"^[a-zA-Z0-9\s,.'-]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(address);
        }

    }
}
