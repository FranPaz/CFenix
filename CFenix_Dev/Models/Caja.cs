using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CFenix_Dev.Models
{
    public class Caja
    {
        public int Id { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal MontoApertura { get; set; }
        public decimal MontoCierre { get; set; }
        // falta id del operador que abre y del que cierra la caja
        public virtual ICollection<MovimientosCaja> MovimientosCajas { get; set; } // 1 a M con MovimientosCaja (muchos)
        public virtual ICollection<Pago> Pagos { get; set; } // 1 a M con Pagos
    }

    public class MovimientosCaja
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        // falta id del operador que hace el movimiento

        // 1 a M con TipoMovCaja (uno)
        public int TipoMovCajaId { get; set; }
        public virtual TipoMovCaja TipoMovCaja { get; set; }

        // 1 a M con Cajas (uno)
        public int CajaId { get; set; }
        public virtual Caja Caja { get; set; }
    }

    public class TipoMovCaja
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Egreso { get; set; } // Egreso = true, Ingreso=False
        public virtual ICollection<MovimientosCaja> MovimientosCajas { get; set; } // 1 a M con Movimientos Cajas (muchos)
        public virtual ICollection<DetallePago> DetallesPagos { get; set; } // 1 a M con DetallePago (muchos)
        
    }
}