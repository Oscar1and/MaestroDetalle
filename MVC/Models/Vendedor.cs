using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Vendedor
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Numero_Identificacion { get; set; }
        public Nullable<int> Codigo_Ciudad { get; set; }
        public virtual Ciudad Ciudad { get; set; }
    }
}