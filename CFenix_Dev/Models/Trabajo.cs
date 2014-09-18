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
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int TipoTrabajoId { get; set; }
        public virtual TipoTrabajo TipoTrabajo {get;set;}

    }

    public class TipoTrabajo
    { 
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public virtual ICollection<Trabajo> Trabajos {get; set;}


    }
}