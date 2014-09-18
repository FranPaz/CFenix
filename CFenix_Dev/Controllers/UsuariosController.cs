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
    [Authorize]
    public class UsuariosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Usuarios
        public IQueryable<ApplicationUser> GetApplicationUsers()
        {
            return db.Users;
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult GetApplicationUser(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApplicationUser(string id, ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationUser.Id)
            {
                return BadRequest();
            }

            db.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
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

        // POST: api/Usuarios
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult PostApplicationUser(ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(applicationUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ApplicationUserExists(applicationUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = applicationUser.Id }, applicationUser);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult DeleteApplicationUser(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            db.Users.Remove(applicationUser);
            db.SaveChanges();

            return Ok(applicationUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationUserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}