using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CFenix_Dev.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoVta { get; set; }

        public virtual ICollection<DetalleVta> DetallesVta { get; set; } // 1 a M con DetalleVta (uno)

        // 1 a M con cliente (uno)
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

    }

    public class DetalleVta
    {    
        public int Id { get; set; }
        public int CodProducto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        //1 a M con Venta (uno)
        public int VentaId { get; set; }
        public virtual Venta Venta { get; set; }

    }
}