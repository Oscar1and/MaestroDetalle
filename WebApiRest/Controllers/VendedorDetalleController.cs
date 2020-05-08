using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiRest.Models;

namespace WebApiRest.Controllers
{
    public class VendedorDetalleController : ApiController
    {
        private MaestroDetalleEntities db = new MaestroDetalleEntities();

        /// <summary>
        /// GET: api/VendedorDetalle - Obtiene la lista de vendedores registrados
        /// </summary>
        /// <returns></returns>
        public IQueryable<VendedorDetalle> GetVendedorDetalles()
        {
            var det = db.VendedorDetalles;
            return det;
        }

        /// <summary>
        /// GET: api/VendedorDetalle/5 - Obtiene el detalle del vendedor solicitado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>  
        [ResponseType(typeof(VendedorDetalle))]
        public IHttpActionResult GetVendedorDetalle(int id)
        {
            VendedorDetalle vendedorDetalle = db.VendedorDetalles.Where(ven => ven.Codigo == id).FirstOrDefault();

            if (vendedorDetalle == null)
            {
                return NotFound();
            }

            return Ok(vendedorDetalle);
        }

        //// PUT: api/VendedorDetalle/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutVendedorDetalle(int id, VendedorDetalle vendedorDetalle)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != vendedorDetalle.Codigo_Ciudad)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(vendedorDetalle).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!VendedorDetalleExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/VendedorDetalle
        //[ResponseType(typeof(VendedorDetalle))]
        //public IHttpActionResult PostVendedorDetalle(VendedorDetalle vendedorDetalle)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.VendedorDetalles.Add(vendedorDetalle);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = vendedorDetalle.Codigo }, vendedorDetalle);
        //}

        //// DELETE: api/VendedorDetalle/5
        //[ResponseType(typeof(VendedorDetalle))]
        //public IHttpActionResult DeleteVendedorDetalle(int id)
        //{
        //    VendedorDetalle vendedorDetalle = db.VendedorDetalles.Find(id);
        //    if (vendedorDetalle == null)
        //    {
        //        return NotFound();
        //    }

        //    db.VendedorDetalles.Remove(vendedorDetalle);
        //    db.SaveChanges();

        //    return Ok(vendedorDetalle);
        //}

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool VendedorDetalleExists(int id)
        //{
        //    return db.VendedorDetalles.Count(e => e.Codigo_Ciudad == id) > 0;
        //}
    }
}