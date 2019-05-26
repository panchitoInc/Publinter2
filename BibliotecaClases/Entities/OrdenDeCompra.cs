using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModule.Entities
{
    public class OrdenDeCompra:DbSet
    {
        [Key, Column("OrdenDeCompraId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdenDeCompraId { get; set; }
        public decimal Salidas { get; set; }
        public decimal Saldo { get; set; }
        public int NumeroDeOrdenDeCompra { get; set; }
        public DateTime Emision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int MedioId { get; set; }
        [ForeignKey("MedioId")]
        public virtual Medio Medio { get; set; }
        public virtual List<Orden> Ordenes { get; set; }
    }
}
