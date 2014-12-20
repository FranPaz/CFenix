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
    public class DeudasController : ApiController
    {
        private CFenix_Context db = new CFenix_Context();

        // GET: api/Deudas
        public IQueryable<Venta> GetVentas()
        {
            return db.Ventas;
        }

        // GET: api/Deudas/5
        [ResponseType(typeof(Venta))]
        public IHttpActionResult GetDeuda(int id) //devuelve todas las compras de un cliente con pagos adeudados
        {
            var listaDeudas = (from v in db.Ventas                               
                               where v.ClienteId == id &&
                               v.MontoVta > (db.Pagos.Where(pa => pa.VentaId == v.Id)
                                                .Select(pa => pa.MontoPago).Sum())                               
                                select v
                               ).ToList();

            if (listaDeudas == null)
            {
                return NotFound();
            }

            return Ok(listaDeudas);
        }

        public IHttpActionResult GetDetalleDeuda(int prmIdCliente, int prmIdVenta) //devuelve todas las compras de un cliente
        {

            var listaDetalleDeudas = (from dv in db.DetallesVta
                                      where dv.VentaId == prmIdVenta
                                      select dv).ToList();
                

            if (listaDetalleDeudas == null)
            {
                return NotFound();
            }

            return Ok(listaDetalleDeudas);
        }

        // PUT: api/Deudas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVenta(int id, Venta venta)
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
                if (!VentaExists(id))
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

        // POST: api/Deudas
        [ResponseType(typeof(Venta))]
        public IHttpActionResult PostVenta(Venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ventas.Add(venta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = venta.Id }, venta);
        }

        // DELETE: api/Deudas/5
        [ResponseType(typeof(Venta))]
        public IHttpActionResult DeleteVenta(int id)
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

        private bool VentaExists(int id)
        {
            return db.Ventas.Count(e => e.Id == id) > 0;
        }
    }
}