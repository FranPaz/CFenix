using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace CFenix_Dev.Models
{
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
    }

    public class TipoEstado
    {        
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<CuentaCorriente> CuentasCorrientes { get; set; }
    }
}