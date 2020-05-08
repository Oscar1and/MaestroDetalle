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
    public class VendedorController : ApiController
    {
        private MaestroDetalleEntities db = new MaestroDetalleEntities();

        //// GET: api/Vendedor
        //public IQueryable<Vendedor> GetVendedors()
        //{
        //    return db.Vendedors;
        //}

        //// GET: api/Vendedor/5
        //[ResponseType(typeof(Vendedor))]
        //public IHttpActionResult GetVendedor(int id)
        //{
        //    Vendedor vendedor = db.Vendedors.Find(id);
        //    if (vendedor == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(vendedor);
        //}

        /// <summary>
        /// PUT: api/Vendedor/5 - Actualiza el registro del vendedor 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vendedor"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendedor(int id, Vendedor vendedor)
        {
            if (id != vendedor.Codigo)
            {
                return BadRequest();
            }

            db.Entry(vendedor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// POST: api/Vendedor - Registra el vendedor
        /// </summary>
        /// <param name="vendedor"></param>
        /// <returns></returns>
        [ResponseType(typeof(Vendedor))]
        public IHttpActionResult PostVendedor(Vendedor vendedor)
        {
            db.Vendedors.Add(vendedor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendedor.Codigo }, vendedor);
        }

        /// <summary>
        /// DELETE: api/Vendedor/5 - Elimina el registro del vendedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Vendedor))]
        public IHttpActionResult DeleteVendedor(int id)
        {
            Vendedor vendedor = db.Vendedors.Find(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            db.Vendedors.Remove(vendedor);
            db.SaveChanges();

            return Ok(vendedor);
        }

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

        /// <summary>
        /// Verifica si existe el registro del vendedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool VendedorExists(int id)
        {
            return db.Vendedors.Count(e => e.Codigo == id) > 0;
        }
    }
}