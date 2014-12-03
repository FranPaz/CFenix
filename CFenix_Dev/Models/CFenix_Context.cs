using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFenix_Dev.Models
{
    public class CFenix_Context:DbContext
    {
        public CFenix_Context() : base("CFenix_Context")
        {
            this.Configuration.LazyLoadingEnabled = false; 
            this.Configuration.ProxyCreationEnabled = false;

            //fpaz: configuracion para la migracion automatica            
            Database.SetInitializer<CFenix_Context>(new DropCreateDatabaseIfModelChanges<CFenix_Context>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<CFenix_Context, CFenix_Dev.Migrations.Configuration>("CFenix_Context"));
        }

        #region Definicion de Tablas DbSet

        public DbSet<CuentaCorriente> Cuentas { get; set; }
        public DbSet<TipoEstado> TiposEstado { get; set; }

        //iafar: definicion de tablas
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Trabajo> Trabajos { get; set; }
        public DbSet<TipoTrabajo> TipoTrabajos { get; set; }        
        public DbSet<CFenix_Dev.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<CFenix_Dev.Models.Venta> Ventas { get; set; }

        public DbSet<DetalleVta> DetallesVta { get; set; }

        public System.Data.Entity.DbSet<CFenix_Dev.Models.Pago> Pagos { get; set; }
        public DbSet<DetallePago> DetallesPagos { get; set; }        

        public System.Data.Entity.DbSet<CFenix_Dev.Models.Caja> Cajas { get; set; }

        public System.Data.Entity.DbSet<CFenix_Dev.Models.TipoMovCaja> TipoMovCajas { get; set; }

        public System.Data.Entity.DbSet<CFenix_Dev.Models.PagoEfectivo> PagosEfectivo { get; set; }

        public System.Data.Entity.DbSet<CFenix_Dev.Models.PagoCC> PagosCC { get; set; }

        public System.Data.Entity.DbSet<CFenix_Dev.Models.Cheque> Cheques { get; set; }

        public System.Data.Entity.DbSet<CFenix_Dev.Models.TipoMovCC> TipoMovCCs { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            //fpaz: para relacion 1 a 1 entre cliente y cuenta corriente defino que ClienteId va a ser ka ForeingKey de la relacion 1 a 1 y asi poder usar el properti cliente para navegacion
            modelBuilder.Entity<CuentaCorriente>()
                        .HasRequired(a => a.Cliente)
                        .WithMany()
                        .HasForeignKey(u => u.ClienteId);

            base.OnModelCreating(modelBuilder);
        }     
    }
   
}