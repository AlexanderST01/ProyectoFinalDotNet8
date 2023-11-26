﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Despacho
    {
        [Key]
        public int DespachoId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int CentroEducativoId { get; set; }
        public int MenuId { get; set; }
        public bool ConfirmacionEntrega { get; set; }
        public string? RecibidoPor { get; set; }
        public DateTime FechaRecepcion { get; set; } = DateTime.Now;
        [ForeignKey("DespachoId")]
        public ICollection<DespachoDetalle> DespachoDetalles { get; set; } = new List<DespachoDetalle>();
    }
}