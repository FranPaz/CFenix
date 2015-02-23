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
    public class ClientesController : ApiController
    {
        private CFenix_Context db = new CFenix_Context();

         //GET: api/Clientes
        public IHttpActionResult GetClientes()
        {
            var clientes = db.Clientes.ToList();            

            if (clientes == null)
            {
                return NotFound();
            }
            return Ok(clientes);
        }
        

        // GET: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult GetCliente(int id)
        {
            Cliente cliente = db.Clientes.Find(id); //fpaz: obtengo el los datos del cliente al que corresponde el id que recibo como parametro

            //fpaz: en esta parte devuelo los datos de la cuenta corriente asociada al clientente
            var cuentaClientes = db.Cuentas
                .Where(cc => cc.ClienteId == cliente.Id) //fpaz: filtro por el id de cliente
                .Include(cc => cc.Cliente)//incluyo la info del cliente asociado
                .Include(cc => cc.TipoEstado)//incluyo la info del tipo de estado
                .FirstOrDefault();

            if (cuentaClientes == null)
            {
                return NotFound();
            }
            
            return Ok(cuentaClientes);
        }

        // PUT: api/Clientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCliente(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.Id)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        [ResponseType(typeof(Cliente))]
        public HttpResponseMessage PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                //fpaz: si el modelo enviado con los datos del cliente es incorrecto devuelve un error
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                //fpaz: agrego el nuevo cliente al contexto
                db.Clientes.Add(cliente);
                
                //fpaz: instancio un obj del tipo cuenta corriente y asigno los valores correspondientes para cada campo
                var cuentaCorriente = new CuentaCorriente() {
                    MontoTotal=0,
                    FechaAlta=DateTime.Now,
                    //fpaz: el estado por defecto en la creacion de una cuenta corriente es Activado
                    TipoEstado = (from te in db.TiposEstado
                                 where te.Descripcion == "Activado"
                                 select te).First(),
                    Cliente = cliente //fpaz: asigo el cliente que agregue al contexto antes, en este punto ya deberia tener su id temporal
                };                

                db.Cuentas.Add(cuentaCorriente); //fpaz: agrego la cc al contexto ya deberia tener incluido al cliente
                db.SaveChanges();                 
                return  Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest,errorMessage);                
            }
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult DeleteCliente(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.Clientes.Remove(cliente);
            db.SaveChanges();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return db.Clientes.Count(e => e.Id == id) > 0;
        }
    }
}