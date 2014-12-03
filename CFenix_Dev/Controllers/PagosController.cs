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
    public class PagosController : ApiController
    {
        private CFenix_Context db = new CFenix_Context();

        // GET: api/Pagos
        public IQueryable<Pago> GetPagoes()
        {
            return db.Pagos;
        }

        // GET: api/Pagos/5
        [ResponseType(typeof(Pago))]
        public IHttpActionResult GetPago(int id)
        {
            //Pago pago = db.Pagos.Find(id);
            //if (pago == null)
            //{
            //    return NotFound();
            //}

            //return Ok(pago);
            var pago = db.Pagos
                .Where(cc => cc.VentaId == id) //fpaz: filtro por el id de cliente
                .Include(cc => cc.DetallesPagos)//incluyo la info del cliente asociado                
                .ToList();
            return Ok(pago);
        }

        // PUT: api/Pagos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPago(int id, Pago pago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pago.Id)
            {
                return BadRequest();
            }

            db.Entry(pago).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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

        // POST: api/Pagos
        [ResponseType(typeof(Pago))]
        public IHttpActionResult PostPago(PagoCustom pagoCustom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Pago pago = new Pago();
                pago = pagoCustom.pago;
                pago.Fecha = DateTime.Now;

                if (pagoCustom.pagoEfectivo != null)
                {
                    PagoEfectivo pagoEfect = new PagoEfectivo();
                    pagoEfect = pagoCustom.pagoEfectivo;
                    pagoEfect.Pago = pago;
                    pagoEfect.TipoMovCaja = db.TipoMovCajas.Find(1);
                    db.PagosEfectivo.Add(pagoEfect);                    
                }

                if (pagoCustom.pagoCC != null)
                {
                    PagoCC pagoCuenta = new PagoCC();
                    pagoCuenta = pagoCustom.pagoCC;
                    pagoCuenta.Pago = pago;
                    pagoCuenta.TipoMovCC = db.TipoMovCCs.Find(1);
                    pagoCuenta.TipoMovCaja = db.TipoMovCajas.Find(2);
                    pagoCuenta.CuentaCorriente = (from cc in db.Cuentas
                                                  where cc.ClienteId == pagoCustom.pago.Venta.ClienteId
                                                  select cc).FirstOrDefault();
                    db.PagosCC.Add(pagoCuenta);
                }

                if (pagoCustom.pagoCheque != null)
                {
                    Cheque pagoCheque = new Cheque();
                    pagoCheque = pagoCustom.pagoCheque;
                    pagoCheque.Pago = pago;
                    pagoCheque.TipoMovCaja = db.TipoMovCajas.Find(3);
                    pagoCheque.FechaEmision = DateTime.Now;
                    pagoCheque.FechaPago = DateTime.Now;
                    db.Cheques.Add(pagoCheque);
                }

                db.Pagos.Add(pago);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
                
            
        }

        // DELETE: api/Pagos/5
        [ResponseType(typeof(Pago))]
        public IHttpActionResult DeletePago(int id)
        {
            Pago pago = db.Pagos.Find(id);
            if (pago == null)
            {
                return NotFound();
            }

            db.Pagos.Remove(pago);
            db.SaveChanges();

            return Ok(pago);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PagoExists(int id)
        {
            return db.Pagos.Count(e => e.Id == id) > 0;
        }
    }
    public class PagoCustom
    {
        public Pago pago { get; set; }
        public PagoEfectivo pagoEfectivo { get; set; }
        public PagoCC pagoCC { get; set; }
        public Cheque pagoCheque { get; set; }
    }
}