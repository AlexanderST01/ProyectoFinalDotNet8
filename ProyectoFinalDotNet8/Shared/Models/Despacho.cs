using System;
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
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un centro educativo")]
        public int CentroEducativoId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un menú")]
        public int MenuId { get; set; }
        public bool ConfirmacionEntrega { get; set; }
        public string? RecibidoPor { get; set; }

        public string? Observacion { get; set; }
        public DateTime FechaRecepcion { get; set; }
        [ForeignKey("DespachoId")]
        public ICollection<DespachoDetalle> DespachoDetalles { get; set; } = new List<DespachoDetalle>();
    }
}