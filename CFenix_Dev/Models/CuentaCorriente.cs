using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace CFenix_Dev.Models
{
    #region Cuenta Corriente
    public class CuentaCorriente
    {        
        public int Id { get; set; }
        public double MontoTotal { get; set; }        
        public DateTime FechaAlta { get; set; }
        public int TipoEstadoId { get; set; }
        public virtual TipoEstado TipoEstado { get; set; }
        
        //fpaz: relacion 1 a 1 con Cliente
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<MovimientoCC> MovimientosCC { get; set; } // 1 a M con MovimientosCC(muchos)
        public virtual ICollection<PagoCC> PagosCC { get; set; } // 1 a M con PagosCC (muchos)
    }
    public class TipoEstado
    {        
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<CuentaCorriente> CuentasCorrientes { get; set; }
    }
    #endregion

    #region Movimientos Cuenta Corriente
    public class MovimientoCC
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        
        //Relacion 1 a M con TipoMovCC (uno)
        public int TipoMovCCId { get; set; }
        public virtual TipoMovCC TipoMovCC { get; set; }

        // 1 a M con Cuenta Corriente (uno)
        public int CuentaCorrienteId { get; set; }
        public virtual CuentaCorriente CuentaCorriente { get; set; }

        //Relacion 1 a M con TipoMovCaja (uno)
        public int TipoMovCajaId { get; set; }
        public virtual TipoMovCaja TipoMovCaja { get; set; }
        
    }

    public class TipoMovCC
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Egreso { get; set; } // Egreso = true, Ingreso = false
        public virtual ICollection<MovimientoCC> MovimientosCC { get; set; } //relacion 1 a M con MovimientosCC (muchos)
        public virtual ICollection<PagoCC> PagosCC { get; set; } // 1 a M con PagosCC
    }

    #endregion
}