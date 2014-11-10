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
        public IHttpActionResult  GetTrabajos()
        {
            var trabajos = db.Trabajos.ToList();
            if (trabajos == null)
            {
                return NotFound();
            }
            return Ok(trabajos);
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

        public class parametrosTrabajo
        {
            public Trabajo trabajo { get; set; }
            public ICollection<Insumo> listInsumo { get; set; }
        }

        // POST: api/Trabajos
        //[ResponseType(typeof(Trabajo))]
        public IHttpActionResult PostTrabajo(parametrosTrabajo paramTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                Trabajo trabajo = new Trabajo();
                trabajo.Nombre = paramTrabajo.trabajo.Nombre;

                //iafar: esto es temporal para cargar el tipo trabajo 1 siempre, despues habra que cargar
                //segun el id
                var auxTipoTrab = db.TipoTrabajos.Find(1);

                trabajo.TipoTrabajo = auxTipoTrab;

                db.Trabajos.Add(trabajo);
                
                foreach (var insumo in paramTrabajo.listInsumo)
                {
                //Insumo ins = new Insumo();

                    var auxInsumo = db.Insumos.Find(insumo.Id);

                    auxInsumo.Trabajos.Add(trabajo); // el hashSet esta en el modelo de insumos, x eso la relacion es al reves
                    //trabajo.Insumos.Add(auxInsumo);//esto solo seria si el hashSet esuviera en el modelo de trabajo

                }

                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
                return BadRequest();
            }
            

            
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