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
using CFenix_Dev.Models;

namespace CFenix_Dev.Controllers
{
    public class TrabajosController : ApiController
    {
        private CFenix_Context db = new CFenix_Context();

        // GET: api/Trabajos
        public IQueryable<Trabajo> GetTrabajos()
        {
            return db.Trabajos;
        }

        // GET: api/Trabajos/5
        [ResponseType(typeof(Trabajo))]
        public IHttpActionResult GetTrabajo(int id)
        {
            Trabajo trabajo = db.Trabajos.Find(id);
            if (trabajo == null)
            {
                return NotFound();
            }

            return Ok(trabajo);
        }

        // PUT: api/Trabajos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrabajo(int id, Trabajo trabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trabajo.Id)
            {
                return BadRequest();
            }

            db.Entry(trabajo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrabajoExists(id))
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

        // POST: api/Trabajos
        [ResponseType(typeof(Trabajo))]
        public IHttpActionResult PostTrabajo(Trabajo trabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trabajos.Add(trabajo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trabajo.Id }, trabajo);
        }

        // DELETE: api/Trabajos/5
        [ResponseType(typeof(Trabajo))]
        public IHttpActionResult DeleteTrabajo(int id)
        {
            Trabajo trabajo = db.Trabajos.Find(id);
            if (trabajo == null)
            {
                return NotFound();
            }

            db.Trabajos.Remove(trabajo);
            db.SaveChanges();

            return Ok(trabajo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrabajoExists(int id)
        {
            return db.Trabajos.Count(e => e.Id == id) > 0;
        }
    }
}