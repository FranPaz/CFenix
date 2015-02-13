using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CFenix_Dev.Models
{
    public class TipoFormaPago
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        // fpaz: 1 a M con DetallePago (muchos)
        public virtual ICollection<DetallePago> DetallesPagos { get; set; }
    }
}