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
    public class CiudadController : ApiController
    {
        private MaestroDetalleEntities db = new MaestroDetalleEntities();

        /// <summary>
        /// GET: api/Ciudad - Obtiene la lista de ciudades registradas 
        /// </summary>
        /// <returns></returns> 
        public IQueryable<Ciudad> GetCiudads()
        {
            return db.Ciudads;
        }

        /// <summary>
        /// GET: api/Ciudad/5 - Obtiene el detalle de la ciudad solicitada
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Ciudad))]
        public IHttpActionResult GetCiudad(int id)
        {
            Ciudad ciudad = db.Ciudads.Find(id);
            if (ciudad == null)
            {
                return NotFound();
            }

            return Ok(ciudad);
        }

        /// <summary>
        /// PUT: api/Ciudad/5 - Actualiza el registro de la ciudad
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ciudad"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCiudad(int id, Ciudad ciudad)
        {
            if (id != ciudad.Codigo_Ciudad)
            {
                return BadRequest();
            }

            db.Entry(ciudad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CiudadExists(id))
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
        /// POST: api/Ciudad - Registra la ciudad
        /// </summary>
        /// <param name="ciudad"></param>
        /// <returns></returns>
        [ResponseType(typeof(Ciudad))]
        public IHttpActionResult PostCiudad(Ciudad ciudad)
        {
            db.Ciudads.Add(ciudad);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ciudad.Codigo_Ciudad }, ciudad);
        }

        /// <summary>
        /// DELETE: api/Ciudad/5 - Elimina el registro de la ciudad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Ciudad))]
        public IHttpActionResult DeleteCiudad(int id)
        {
            Ciudad ciudad = db.Ciudads.Find(id);
            if (ciudad == null)
            {
                return NotFound();
            }

            db.Ciudads.Remove(ciudad);
            db.SaveChanges();

            return Ok(ciudad);
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
        /// Verifica si existe el registro de la ciudad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CiudadExists(int id)
        {
            return db.Ciudads.Count(e => e.Codigo_Ciudad == id) > 0;
        }
    }
}