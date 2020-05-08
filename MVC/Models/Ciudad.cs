using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Ciudad
    {
       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ciudad()
        {
            this.Vendedors = new HashSet<Vendedor>();
        }

        public int Codigo_Ciudad { get; set; }
        [Required(ErrorMessage = "Debe ingresar el nombre de la ciudad")]
        public string Descripcion { get; set; }

       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vendedor> Vendedors { get; set; }
    }
}