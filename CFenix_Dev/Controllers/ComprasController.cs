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
    public class ComprasController : ApiController
    {
        private CFenix_Context db = new CFenix_Context();

        // GET: api/Compras
        public IQueryable<Venta> GetCompras()
        {
            return db.Ventas;
        }

        // GET: api/Compras/5
        [ResponseType(typeof(Venta))]
        public IHttpActionResult GetCompra(int id)
        {
            var listaCompras = (from v in db.Ventas
                               where v.ClienteId == id 
                               select v
                               ).ToList();

            if (listaCompras == null)
            {
                return NotFound();
            }

            return Ok(listaCompras);
        }

        // PUT: api/Compras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompra(int id, Venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != venta.Id)
            {
                return BadRequest();
            }

            db.Entry(venta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(id))
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

        // POST: api/Compras
        [ResponseType(typeof(Venta))]
        public IHttpActionResult PostCompra(Venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ventas.Add(venta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = venta.Id }, venta);
        }

        // DELETE: api/Compras/5
        [ResponseType(typeof(Venta))]
        public IHttpActionResult DeleteCompra(int id)
        {
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return NotFound();
            }

            db.Ventas.Remove(venta);
            db.SaveChanges();

            return Ok(venta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompraExists(int id)
        {
            return db.Ventas.Count(e => e.Id == id) > 0;
        }
    }
}