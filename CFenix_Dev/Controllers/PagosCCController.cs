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
    public class PagosCCController : ApiController
    {
        private CFenix_Context db = new CFenix_Context();

        // GET: api/PagosCC
        public IQueryable<PagoCC> GetDetallePagoes()
        {
            return db.PagosCC;
        }

        // GET: api/PagosCC/5
        [ResponseType(typeof(PagoCC))]
        public IHttpActionResult GetPagoCC(int id)
        {
            PagoCC pagoCC = db.PagosCC.Find(id);
            if (pagoCC == null)
            {
                return NotFound();
            }

            return Ok(pagoCC);
        }

        // PUT: api/PagosCC/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPagoCC(int id, PagoCC pagoCC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pagoCC.Id)
            {
                return BadRequest();
            }

            db.Entry(pagoCC).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoCCExists(id))
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

        // POST: api/PagosCC
        [ResponseType(typeof(PagoCC))]
        public IHttpActionResult PostPagoCC(PagoCC pagoCC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PagosCC.Add(pagoCC);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pagoCC.Id }, pagoCC);
        }

        // DELETE: api/PagosCC/5
        [ResponseType(typeof(PagoCC))]
        public IHttpActionResult DeletePagoCC(int id)
        {
            PagoCC pagoCC = db.PagosCC.Find(id);
            if (pagoCC == null)
            {
                return NotFound();
            }

            db.PagosCC.Remove(pagoCC);
            db.SaveChanges();

            return Ok(pagoCC);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PagoCCExists(int id)
        {
            return db.PagosCC.Count(e => e.Id == id) > 0;
        }
    }
}