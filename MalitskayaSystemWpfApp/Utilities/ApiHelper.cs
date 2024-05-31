using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Windows;
using System.IO;
using System.Security.Policy;
using System.Runtime.InteropServices.WindowsRuntime;
using Newtonsoft.Json;
using MalitskayaSystemWpfApp.Model;

namespace MalitskayaSystemWpfApp.Utilities
{
    internal class ApiHelper
    {
        //private static string Url = "http://127.0.0.1:8000/Models";
        //private static string token = "e95a3684b9982fcfd46eea716707f80cef515906eb49c4cb961dfde39a41ce21";
        public static T Put<T>(string json, string model) where T: new()
        {
            HttpClient client = new HttpClient();
            HttpContent body = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);
            HttpResponseMessage response = client.PutAsync($"{Url}?Model={model}", body).Result;
            Response<T> answer = JsonConvert.DeserializeObject<Response<T>>(response.Content.ReadAsStringAsync().Result);
            if (answer.status == 200) return answer.response != null ? answer.response: new T();
            else MessageBox.Show(answer.error.ToString());
            return answer.response;
        }
        
        public static T Get<T>(string model, int id = 0, string articul = "", int orderID = 0) 
        {
            
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);
            string param = id != 0 ? $"id={id}&" : orderID != 0 ? $"order_id={orderID}&" : articul != "" ? $"articul={articul} &": "";
            HttpResponseMessage response = client.GetAsync($"{Url}?{param}Model={model}").Result;
            Response<T> content = JsonConvert.DeserializeObject<Response<T>>(response.Content.ReadAsStringAsync().Result);
            if (content.status == 200) return content.response;
            else MessageBox.Show(content.error.ToString());
            return content.response;
        }


        public static T Post<T>(string json, string model)
        {
            HttpClient client = new HttpClient();
            HttpContent body = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);
            HttpResponseMessage response = client.PostAsync($"{Url}?Model={model}", body).Result;
            Response<T> content = JsonConvert.DeserializeObject<Response<T>>(response.Content.ReadAsStringAsync().Result);
            if (content.status == 201) return content.response;
            else MessageBox.Show(content.error.ToString());
            return content.response;
        }

        public static bool Delete(string model, int id = 0, string articul = "")
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);
            string param = id != 0 ? $"?id={id}" : articul != "" ? $"?articul={articul}" : "";
            HttpResponseMessage response = client.DeleteAsync($"{Url}{param}&Model={model}").Result;
            Response<List<string>> content = JsonConvert.DeserializeObject<Response<List<string>>>(response.Content.ReadAsStringAsync().Result);
            if (content.status != 200) 
            {
                if (content.error != null) MessageBox.Show(content.error.ToString());
                return false;
            }
            return true;
        }
    }
}
