using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CFenix_Dev.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CFenix_Context", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<CFenix_Dev.Models.Insumo> Insumoes { get; set; }

        public System.Data.Entity.DbSet<CFenix_Dev.Models.Proveedor> Proveedors { get; set; }

        public System.Data.Entity.DbSet<CFenix_Dev.Models.Trabajo> Trabajoes { get; set; }

        public System.Data.Entity.DbSet<CFenix_Dev.Models.TipoTrabajo> TipoTrabajoes { get; set; }

        //public System.Data.Entity.DbSet<CFenix_Dev.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}