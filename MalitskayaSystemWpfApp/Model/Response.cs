using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalitskayaSystemWpfApp.Model
{
    internal class Response<T>
    {
        public int status {  get; set; }
        public string error { get; set; }
        public T response { get; set; }
    }
}
