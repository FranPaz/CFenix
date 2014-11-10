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
    public class InsumosController : ApiController
    {
        private CFenix_Context db = new CFenix_Context();

        // GET: api/Insumos
        public IHttpActionResult GetInsumos()
        {
            var insumos = db.Insumos.ToList();
            if (insumos == null)
            {
                return NotFound();
            }
            return Ok(insumos);
        }

        // GET: api/Insumos/5
        [ResponseType(typeof(Insumo))]
        public IHttpActionResult GetInsumo(int id)
        {
            Insumo insumo = db.Insumos.Find(id);
            if (insumo == null)
            {
                return NotFound();
            }

            return Ok(insumo);
        }

        // PUT: api/Insumos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInsumo(int id, Insumo insumo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != insumo.Id)
            {
                return BadRequest();
            }

            db.Entry(insumo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsumoExists(id))
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

        // POST: api/Insumos
        //[ResponseType(typeof(Insumo))]
        public IHttpActionResult PostInsumo(Insumo insumo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                db.Insumos.Add(insumo);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                
                ex.Message.ToString();
                return BadRequest();

            }
            

            
        }

        // DELETE: api/Insumos/5
        [ResponseType(typeof(Insumo))]
        public IHttpActionResult DeleteInsumo(int id)
        {
            Insumo insumo = db.Insumos.Find(id);
            if (insumo == null)
            {
                return NotFound();
            }

            db.Insumos.Remove(insumo);
            db.SaveChanges();

            return Ok(insumo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InsumoExists(int id)
        {
            return db.Insumos.Count(e => e.Id == id) > 0;
        }
    }
}