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
    public class PagosEfectivoController : ApiController
    {
        private CFenix_Context db = new CFenix_Context();

        // GET: api/PagosEfectivo
        public IQueryable<PagoEfectivo> GetDetallePagoes()
        {
            return db.PagosEfectivo;
        }

        // GET: api/PagosEfectivo/5
        [ResponseType(typeof(PagoEfectivo))]
        public IHttpActionResult GetPagoEfectivo(int id)
        {
            PagoEfectivo pagoEfectivo = db.PagosEfectivo.Find(id);
            if (pagoEfectivo == null)
            {
                return NotFound();
            }

            return Ok(pagoEfectivo);
        }

        // PUT: api/PagosEfectivo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPagoEfectivo(int id, PagoEfectivo pagoEfectivo, Pago pago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pagoEfectivo.Id)
            {
                return BadRequest();
            }

            db.Entry(pagoEfectivo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoEfectivoExists(id))
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

        // POST: api/PagosEfectivo
        [ResponseType(typeof(PagoEfectivo))]
        public IHttpActionResult PostPagoEfectivo(PagoEfectivo pagoEfectivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PagosEfectivo.Add(pagoEfectivo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pagoEfectivo.Id }, pagoEfectivo);
        }

        // DELETE: api/PagosEfectivo/5
        [ResponseType(typeof(PagoEfectivo))]
        public IHttpActionResult DeletePagoEfectivo(int id)
        {
            PagoEfectivo pagoEfectivo = db.PagosEfectivo.Find(id);
            if (pagoEfectivo == null)
            {
                return NotFound();
            }

            db.PagosEfectivo.Remove(pagoEfectivo);
            db.SaveChanges();

            return Ok(pagoEfectivo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PagoEfectivoExists(int id)
        {
            return db.PagosEfectivo.Count(e => e.Id == id) > 0;
        }
    }
}