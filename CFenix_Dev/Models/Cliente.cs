using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CFenix_Dev.Models
{
    public class Cliente
    {        
        public int Id { get; set; }
        public string Nombre_RazonSocial { get; set; }
        public string Cuit_Cuil { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }
    }
}