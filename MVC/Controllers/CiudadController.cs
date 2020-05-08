using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class CiudadController : Controller
    {
        /// <summary>
        /// GET: Ciudad - Carga la lista de ciudades del sistema
        /// </summary>
        /// <returns></returns>        
        public ActionResult Index()
        {
            IEnumerable<Ciudad> ciudadList;

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Ciudad").Result;

            ciudadList = response.Content.ReadAsAsync<IEnumerable<Ciudad>>().Result;

            return View(ciudadList);

        }

        /// <summary>
        /// Obtiene la ciudad para edicion o registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Ciudad());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Ciudad/" + id.ToString()).Result;

                return View(response.Content.ReadAsAsync<Ciudad>().Result);
            }

        }

        /// <summary>
        /// Actualiza o Registra una ciudad
        /// </summary>
        /// <param name="ciudad"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrEdit(Ciudad ciudad)
        {

            if (ciudad.Codigo_Ciudad == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Ciudad", ciudad).Result;

                TempData["SuccessMessage"] = "La ciudad se registro satisfactoriamente";
                return RedirectToAction("Index");
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Ciudad/" + ciudad.Codigo_Ciudad, ciudad).Result;

                TempData["SuccessMessage"] = "La ciudad se actualizo satisfactoriamente";
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// Elimina un registro ciudad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Ciudad/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "La ciudad se elimino satisfactoriamente";
            return RedirectToAction("Index");
        }



        public ActionResult GetList()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Ciudad").Result;

            var ciudadList = response.Content.ReadAsAsync<IEnumerable<Ciudad>>().Result;

            List<SelectListItem> PList = new List<SelectListItem>();
            PList = ciudadList.Select(i => new SelectListItem()
            {
                Text = i.Descripcion,
                Value = i.Codigo_Ciudad.ToString()
            }).ToList();


            ViewBag.Opciones = PList;

            return Json(PList, JsonRequestBehavior.AllowGet);
        }
    }
}