namespace CFenix_Dev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregoRelacionDetalleVentaconTrabajo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cajas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaApertura = c.DateTime(nullable: false),
                        FechaCierre = c.DateTime(nullable: false),
                        MontoApertura = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontoCierre = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovimientosCajas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descripcion = c.String(),
                        TipoMovCajaId = c.Int(nullable: false),
                        CajaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cajas", t => t.CajaId, cascadeDelete: true)
                .ForeignKey("dbo.TipoMovCajas", t => t.TipoMovCajaId, cascadeDelete: true)
                .Index(t => t.TipoMovCajaId)
                .Index(t => t.CajaId);
            
            CreateTable(
                "dbo.TipoMovCajas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Egreso = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetallePagoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PagoId = c.Int(nullable: false),
                        TipoMovCajaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pagoes", t => t.PagoId, cascadeDelete: true)
                .ForeignKey("dbo.TipoMovCajas", t => t.TipoMovCajaId, cascadeDelete: true)
                .Index(t => t.PagoId)
                .Index(t => t.TipoMovCajaId);
            
            CreateTable(
                "dbo.Pagoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        MontoPago = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VentaId = c.Int(nullable: false),
                        Caja_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .ForeignKey("dbo.Cajas", t => t.Caja_Id)
                .Index(t => t.VentaId)
                .Index(t => t.Caja_Id);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        MontoVta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre_RazonSocial = c.String(),
                        Cuit_Cuil = c.String(),
                        Direccion = c.String(),
                        Telefono = c.String(),
                        Mail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetalleVtas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Cantidad = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VentaId = c.Int(nullable: false),
                        TrabajoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trabajoes", t => t.TrabajoId, cascadeDelete: true)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.VentaId)
                .Index(t => t.TrabajoId);
            
            CreateTable(
                "dbo.Trabajoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UMedida = c.String(),
                        TipoTrabajoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoTrabajoes", t => t.TipoTrabajoId, cascadeDelete: true)
                .Index(t => t.TipoTrabajoId);
            
            CreateTable(
                "dbo.Insumoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        UMedida = c.String(),
                        CantStock = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PtoRepo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecioCompra = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecioVenta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Proveedor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Proveedors", t => t.Proveedor_Id)
                .Index(t => t.Proveedor_Id);
            
            CreateTable(
                "dbo.TipoTrabajoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CuentaCorrientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MontoTotal = c.Double(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        TipoEstadoId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.TipoEstadoes", t => t.TipoEstadoId, cascadeDelete: true)
                .Index(t => t.TipoEstadoId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.MovimientoCCs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descripcion = c.String(),
                        TipoMovCCId = c.Int(nullable: false),
                        CuentaCorrienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CuentaCorrientes", t => t.CuentaCorrienteId, cascadeDelete: true)
                .ForeignKey("dbo.TipoMovCCs", t => t.TipoMovCCId, cascadeDelete: true)
                .Index(t => t.TipoMovCCId)
                .Index(t => t.CuentaCorrienteId);
            
            CreateTable(
                "dbo.TipoMovCCs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Egreso = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipoEstadoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Proveedors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Dir = c.String(),
                        tel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InsumoTrabajoes",
                c => new
                    {
                        Insumo_Id = c.Int(nullable: false),
                        Trabajo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Insumo_Id, t.Trabajo_Id })
                .ForeignKey("dbo.Insumoes", t => t.Insumo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Trabajoes", t => t.Trabajo_Id, cascadeDelete: true)
                .Index(t => t.Insumo_Id)
                .Index(t => t.Trabajo_Id);
            
            CreateTable(
                "dbo.Cheques",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NroCheque = c.String(),
                        FechaEmision = c.DateTime(nullable: false),
                        FechaPago = c.DateTime(nullable: false),
                        Librador = c.String(),
                        Banco = c.String(),
                        Observaciones = c.String(),
                        Cobrado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DetallePagoes", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.PagosCC",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TipoMovCCId = c.Int(nullable: false),
                        CuentaCorrienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DetallePagoes", t => t.Id)
                .ForeignKey("dbo.TipoMovCCs", t => t.TipoMovCCId, cascadeDelete: true)
                .ForeignKey("dbo.CuentaCorrientes", t => t.CuentaCorrienteId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TipoMovCCId)
                .Index(t => t.CuentaCorrienteId);
            
            CreateTable(
                "dbo.PagosEfectivo",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DetallePagoes", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PagosEfectivo", "Id", "dbo.DetallePagoes");
            DropForeignKey("dbo.PagosCC", "CuentaCorrienteId", "dbo.CuentaCorrientes");
            DropForeignKey("dbo.PagosCC", "TipoMovCCId", "dbo.TipoMovCCs");
            DropForeignKey("dbo.PagosCC", "Id", "dbo.DetallePagoes");
            DropForeignKey("dbo.Cheques", "Id", "dbo.DetallePagoes");
            DropForeignKey("dbo.Insumoes", "Proveedor_Id", "dbo.Proveedors");
            DropForeignKey("dbo.Pagoes", "Caja_Id", "dbo.Cajas");
            DropForeignKey("dbo.MovimientosCajas", "TipoMovCajaId", "dbo.TipoMovCajas");
            DropForeignKey("dbo.CuentaCorrientes", "TipoEstadoId", "dbo.TipoEstadoes");
            DropForeignKey("dbo.MovimientoCCs", "TipoMovCCId", "dbo.TipoMovCCs");
            DropForeignKey("dbo.MovimientoCCs", "CuentaCorrienteId", "dbo.CuentaCorrientes");
            DropForeignKey("dbo.CuentaCorrientes", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.DetallePagoes", "TipoMovCajaId", "dbo.TipoMovCajas");
            DropForeignKey("dbo.Pagoes", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.DetalleVtas", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.Trabajoes", "TipoTrabajoId", "dbo.TipoTrabajoes");
            DropForeignKey("dbo.InsumoTrabajoes", "Trabajo_Id", "dbo.Trabajoes");
            DropForeignKey("dbo.InsumoTrabajoes", "Insumo_Id", "dbo.Insumoes");
            DropForeignKey("dbo.DetalleVtas", "TrabajoId", "dbo.Trabajoes");
            DropForeignKey("dbo.Ventas", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.DetallePagoes", "PagoId", "dbo.Pagoes");
            DropForeignKey("dbo.MovimientosCajas", "CajaId", "dbo.Cajas");
            DropIndex("dbo.PagosEfectivo", new[] { "Id" });
            DropIndex("dbo.PagosCC", new[] { "CuentaCorrienteId" });
            DropIndex("dbo.PagosCC", new[] { "TipoMovCCId" });
            DropIndex("dbo.PagosCC", new[] { "Id" });
            DropIndex("dbo.Cheques", new[] { "Id" });
            DropIndex("dbo.InsumoTrabajoes", new[] { "Trabajo_Id" });
            DropIndex("dbo.InsumoTrabajoes", new[] { "Insumo_Id" });
            DropIndex("dbo.MovimientoCCs", new[] { "CuentaCorrienteId" });
            DropIndex("dbo.MovimientoCCs", new[] { "TipoMovCCId" });
            DropIndex("dbo.CuentaCorrientes", new[] { "ClienteId" });
            DropIndex("dbo.CuentaCorrientes", new[] { "TipoEstadoId" });
            DropIndex("dbo.Insumoes", new[] { "Proveedor_Id" });
            DropIndex("dbo.Trabajoes", new[] { "TipoTrabajoId" });
            DropIndex("dbo.DetalleVtas", new[] { "TrabajoId" });
            DropIndex("dbo.DetalleVtas", new[] { "VentaId" });
            DropIndex("dbo.Ventas", new[] { "ClienteId" });
            DropIndex("dbo.Pagoes", new[] { "Caja_Id" });
            DropIndex("dbo.Pagoes", new[] { "VentaId" });
            DropIndex("dbo.DetallePagoes", new[] { "TipoMovCajaId" });
            DropIndex("dbo.DetallePagoes", new[] { "PagoId" });
            DropIndex("dbo.MovimientosCajas", new[] { "CajaId" });
            DropIndex("dbo.MovimientosCajas", new[] { "TipoMovCajaId" });
            DropTable("dbo.PagosEfectivo");
            DropTable("dbo.PagosCC");
            DropTable("dbo.Cheques");
            DropTable("dbo.InsumoTrabajoes");
            DropTable("dbo.Proveedors");
            DropTable("dbo.TipoEstadoes");
            DropTable("dbo.TipoMovCCs");
            DropTable("dbo.MovimientoCCs");
            DropTable("dbo.CuentaCorrientes");
            DropTable("dbo.TipoTrabajoes");
            DropTable("dbo.Insumoes");
            DropTable("dbo.Trabajoes");
            DropTable("dbo.DetalleVtas");
            DropTable("dbo.Clientes");
            DropTable("dbo.Ventas");
            DropTable("dbo.Pagoes");
            DropTable("dbo.DetallePagoes");
            DropTable("dbo.TipoMovCajas");
            DropTable("dbo.MovimientosCajas");
            DropTable("dbo.Cajas");
        }
    }
}
