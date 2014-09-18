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
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string UMedida { get; set; }
        public decimal CantStock { get; set; }
        public decimal PtoRepo { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int ProveedorId { get; set; }
        public virtual Proveedor proveedores { get; set; }
    }

    public class Proveedor
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Dir { get; set; }
        public string tel { get; set; }
        public virtual ICollection<Insumo> Insumos { get; set; }
        
    }

}