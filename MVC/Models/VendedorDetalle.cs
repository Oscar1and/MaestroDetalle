using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class VendedorDetalle
    {
        public int Codigo { get; set; }
        public int Codigo_Ciudad { get; set; }
        [DisplayName("Ciudad")]
        [Required(ErrorMessage = "Debe seleccionar la ciudad del vendedor")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Debe ingresar el Nombre del Vendedor")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe ingresar el Apellido del Vendedor")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Debe ingresar el Número de identificación del Vendedor")]
        public string Numero_Identificacion { get; set; }
    }
}