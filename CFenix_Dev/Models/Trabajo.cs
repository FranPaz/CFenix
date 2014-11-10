using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//iafar:ultima modificacion 12-09-2014

namespace CFenix_Dev.Models
{
    public class Trabajo
    {
        public Trabajo()
        {

        }

        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        //iafar: relacion 1 a muchos con TipoTrabajo(1)
        public int TipoTrabajoId { get; set; }
        public virtual TipoTrabajo TipoTrabajo {get;set;}
        //iafar:relacion 1 a muchos con insumos(muchos)
        public virtual ICollection<Insumo> Insumos { get; set; }
    }

    public class TipoTrabajo
    { 
        
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        //iafar:relacion 1 a muchos con trabajo(muchos)
        public virtual ICollection<Trabajo> Trabajos {get; set;}


    }
}