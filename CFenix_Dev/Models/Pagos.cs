using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CFenix_Dev.Models
{
    #region Pago
    public class Pago
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoPago { get; set; }

        //Relacion 1 a M con Ventas (uno)
        public int VentaId { get; set; }
        public virtual Venta Venta { get; set; }

        public virtual ICollection<DetallePago> DetallesPagos { get; set; } // relacion 1 a M con DetallePagos (muchos)

        
    }
    #endregion

    #region Formas de Pago
    public abstract class DetallePago
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }

        //relacion 1 a M con Pagos (uno)
        public int PagoId { get; set; }
        public virtual Pago Pago { get; set; }

        // 1 a M con Caja (uno)
        public int CajaId { get; set; }
        public virtual Caja Caja { get; set; }

        //1 a M con TipoFormaPago (uno)
        public int TipoFormaPagoId { get; set; }
        public virtual TipoFormaPago TipoFormaPago { get; set; }

    }

    [Table("Cheques")]
    public class Cheque:DetallePago
    {
        public string NroCheque { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaPago { get; set; }
        public string Librador { get; set; } // es el que hace el cheque
        public string Banco { get; set; }
        public string Observaciones { get; set; }
        public bool Cobrado { get; set; } // cobrado= true,no cobrado= false
    }    

    [Table("PagosEfectivo")]
    public class PagoEfectivo : DetallePago
    {   
        // 1 a M con MovimientosCaja (muchos)               
        public virtual ICollection<MovimientosCaja> MovimientosCaja { get; set; }
    }

    [Table("PagosCC")]
    public class PagoCC : DetallePago
    {
        // 1 a M con TipoMovCC (uno)
        public int TipoMovCCId { get; set; }
        public virtual TipoMovCC TipoMovCC { get; set; }

        //1 a M con CuentaCorriente
        public int CuentaCorrienteId { get; set; }
        public virtual CuentaCorriente CuentaCorriente { get; set; }     

    }
    #endregion
}