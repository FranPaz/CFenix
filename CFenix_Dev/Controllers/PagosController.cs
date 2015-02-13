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
            var pago = db.Pagos
                .Where(cc => cc.VentaId == id)
                .Include(cc => cc.DetallesPagos)
                .Include(cc => cc.DetallesPagos.Select(t => t.TipoFormaPago))
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

                var idCajaActiva = (from c in db.Cajas // variable donde voy a guardar el id de la caja que actualmente esta abierta
                                    where c.Abierto == true
                                    select c.Id).FirstOrDefault();

                if (pagoCustom.pagoEfectivo != null)
                {
                    // alta de Pago en efectivo
                    PagoEfectivo pagoEfect = new PagoEfectivo();
                    pagoEfect = pagoCustom.pagoEfectivo;
                    pagoEfect.PagoId = pago.Id;
                    pagoEfect.CajaId = idCajaActiva;
                    pagoEfect.TipoFormaPagoId = (from tp in db.TiposFormasPago
                                                 where tp.Descripcion.Contains("Efectivo")
                                                 select tp.Id).FirstOrDefault();
                    //db.PagosEfectivo.Add(pagoEfect);

                    // alta del movimientoCaja
                    MovimientosCaja movCaja = new MovimientosCaja();
                    movCaja.Monto = pagoCustom.pagoEfectivo.Monto;
                    movCaja.Descripcion = "Pago por compra en efectivo";
                    movCaja.TipoMovCaja = db.TipoMovCajas.Find(1);
                    movCaja.CajaId = idCajaActiva;
                    //movCaja.PagoEfectivoId = pagoEfect.Id;
                    movCaja.PagoEfectivo = pagoEfect;
                    db.MovimientosCajas.Add(movCaja);
                }

                if (pagoCustom.pagoCC != null)
                {
                    PagoCC pagoCuenta = new PagoCC();
                    pagoCuenta = pagoCustom.pagoCC;
                    pagoCuenta.PagoId = pago.Id;
                    pagoCuenta.TipoMovCC = db.TipoMovCCs.Find(1);                    
                    pagoCuenta.CuentaCorriente = (from cc in db.Cuentas
                                                  where cc.ClienteId == pagoCustom.pago.Venta.ClienteId
                                                  select cc).FirstOrDefault();

                    pagoCuenta.CajaId = idCajaActiva;                    
                    pagoCuenta.TipoFormaPagoId = (from tp in db.TiposFormasPago
                                                 where tp.Descripcion.Contains("Cuenta")
                                                 select tp.Id).FirstOrDefault();
                    db.PagosCC.Add(pagoCuenta);
                }

                if (pagoCustom.pagoCheque != null)
                {
                    Cheque pagoCheque = new Cheque();
                    pagoCheque = pagoCustom.pagoCheque;
                    pagoCheque.PagoId = pago.Id;
                    pagoCheque.FechaEmision = pagoCustom.pagoCheque.FechaEmision;
                    pagoCheque.FechaPago = pagoCustom.pagoCheque.FechaPago;
                    pagoCheque.CajaId = idCajaActiva;
                    pagoCheque.TipoFormaPagoId = (from tp in db.TiposFormasPago
                                                 where tp.Descripcion.Contains("Cheque")
                                                 select tp.Id).FirstOrDefault();
                    db.Cheques.Add(pagoCheque);
                }

                pago.VentaId = pagoCustom.pago.Venta.Id;
                pago.Venta = null;

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