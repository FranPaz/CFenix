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

            //FAQ: No se puede usar este tipo de busqueda porque define un model insumo y al modificar la entrada
            //a la base de datos, da error de uso de entidad duplicada
            //var insumoOld = db.Insumos.Find(id).Nombre;

            //Expresion LinQ
            //iafar: almaceno el nombre anterior del insumo para buscar el trabajo del mismo nombre y modificarlo
            var nomViejo = (from i in db.Insumos
                            where i.Id == id
                            select i.Nombre)
                            .First();

            //iafar: modifica el estado del insumo de la base de datos
            //no se debe usar esto antes de guardarlo porque modifica todos los model del tipo insumo
            db.Entry(insumo).State = EntityState.Modified;

            try
            {
               

                var trabajo = db.Trabajos
                    .Where(t => t.Nombre == nomViejo)
                    .FirstOrDefault();

                trabajo.Nombre = insumo.Nombre;

                db.Entry(trabajo).State = EntityState.Modified;


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
        //public IHttpActionResult PostInsumo(Insumo insumo)
        public IHttpActionResult PostInsumo(Trabajo trabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                
                //cargo un objeto trabajo con todos los datos
                //Trabajo trabajo = new Trabajo();
                //trabajo = prmTrabajo;
                

                trabajo.TipoTrabajo = db.TipoTrabajos.Find(1);

                
                //foreach (var item in prmTrabajo.Insumos)
                //{
                //    Insumo insumo = new Insumo();

                //    insumo.Nombre = item.Nombre;
                //    insumo.UMedida = item.UMedida;
                //    insumo.PrecioCompra = item.PrecioCompra;
                //    insumo.PrecioVenta = item.PrecioVenta;
                //    insumo.CantStock = item.CantStock;
                //    insumo.PtoRepo = item.PtoRepo;

                //    trabajo.Insumos.Add(insumo);
                //}
               
                db.Trabajos.Add(trabajo);
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