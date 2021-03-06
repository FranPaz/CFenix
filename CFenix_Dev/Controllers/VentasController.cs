﻿using System;
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
    public class VentasController : ApiController
    {
        private CFenix_Context db = new CFenix_Context();

        // GET: api/Ventas
        public IHttpActionResult GetVentas()
        {
            var ventas = db.Ventas.ToList();
            if (ventas == null)
            {
                return BadRequest("No Existen Ventas");
            }
            return Ok(ventas);
        }

        // GET: api/Ventas/5
        [ResponseType(typeof(Venta))]
        public IHttpActionResult GetVenta(int id) 
        {
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return NotFound();
            }

            return Ok(venta);
        }

        // PUT: api/Ventas/5
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

        // POST: api/Ventas
        [ResponseType(typeof(Venta))]
        public IHttpActionResult PostVenta(Venta prmVenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {   
                prmVenta.Fecha = DateTime.Now;
                prmVenta.MontoVta = prmVenta.DetallesVta
                                .Sum(x => x.Subtotal);

                db.Ventas.Add(prmVenta);
                db.SaveChanges();

                return Ok(prmVenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            
        }

        // DELETE: api/Ventas/5
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