using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//iafar: ultima modificacion 12-09-2014

namespace CFenix_Dev.Models
{
    public class Insumo
    {
        
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string UMedida { get; set; }
        public decimal CantStock { get; set; }
        public decimal PtoRepo { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        
        //iafar:relacion 1 a muchos con proveedor(1)
        public int ProveedorId { get; set; }
        public virtual Proveedor proveedores { get; set; }

        //iafar:relacion 1 a muchos con trabajo(1)
        public int TrabajoId { get; set; }
        public virtual Trabajo trabajos { get; set; }

    }

    public class Proveedor

    {
        
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Dir { get; set; }
        public string tel { get; set; }
        //iafar:relacion 1 a muchos con insumos(muchos)
        public virtual ICollection<Insumo> Insumos { get; set; }
        
    }

}