using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModule.Entities
{
    public class Orden
    {
        [Key, Column("OrdenId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdenId { get; set; }

        public int NroOrden { get; set; }

        /// <summary>
        /// Medio Navigation
        /// </summary>
        public int MedioId { get; set; }

        [ForeignKey("MedioId")]
        public Medio Medio { get; set; }

        public DateTime Emision { get; set; }

        public decimal Total { get; set; }

        public virtual List<LineaOrden> LineasOrden { get; set; }
    }
}
