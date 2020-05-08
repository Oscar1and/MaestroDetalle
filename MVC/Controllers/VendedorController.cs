using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class VendedorController : Controller
    {
        /// <summary>
        /// GET: VendedorDetalle - Carga la lista de vendedores del sistema 
        /// </summary>
        /// <returns></returns>        
        public ActionResult Index()
        {
            IEnumerable<VendedorDetalle> vendedorList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("VendedorDetalle").Result;

            vendedorList = response.Content.ReadAsAsync<IEnumerable<VendedorDetalle>>().Result;

            return View(vendedorList);
        }

        /// <summary>
        /// Obtiene el vendedor para edicion o registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddOrEdit(int id = 0)
        {
            HttpResponseMessage responsecity = GlobalVariables.WebApiClient.GetAsync("Ciudad").Result;

            var ciudadList = responsecity.Content.ReadAsAsync<IEnumerable<Ciudad>>().Result;

            List<SelectListItem> PList = new List<SelectListItem>();
            PList = ciudadList.Select(i => new SelectListItem()
            {
                Text = i.Descripcion,
                Value = i.Codigo_Ciudad.ToString()
            }).ToList();


            ViewBag.Opciones = PList;

            if (id == 0)
            {
                return View(new VendedorDetalle());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("VendedorDetalle/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<VendedorDetalle>().Result);
            }

        }

        /// <summary>
        /// Actualiza o Registra un vendedor
        /// </summary>
        /// <param name="vendedorDetalle"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrEdit(VendedorDetalle vendedorDetalle)
        {
            // Instancia el objeto vendedor
            Vendedor vendedor = new Vendedor();

            // Adiciona las propiedades para registro o edicion
            vendedor.Nombre = vendedorDetalle.Nombre;
            vendedor.Apellido = vendedorDetalle.Apellido;
            vendedor.Numero_Identificacion = vendedorDetalle.Numero_Identificacion;
            vendedor.Codigo_Ciudad = int.Parse(vendedorDetalle.Descripcion);
            vendedor.Codigo = vendedorDetalle.Codigo;


            // Verifica si debe registrar o actualizar
            if (vendedorDetalle.Codigo == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Vendedor", vendedor).Result;

                TempData["SuccessMessage"] = "El vendedor se registro satisfactoriamente";
                return RedirectToAction("Index");
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Vendedor/" + vendedorDetalle.Codigo, vendedor).Result;

                TempData["SuccessMessage"] = "El vendedor se actualizo satisfactoriamente";
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// Elimina un registro vendedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Vendedor/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "El vendedor se elimino satisfactoriamente";
            return RedirectToAction("Index");
        }
    }
}