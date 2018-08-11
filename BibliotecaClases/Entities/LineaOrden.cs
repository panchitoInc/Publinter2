using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModule.Entities
{
    public class LineaOrden
    {
        [Key, Column("LineaOrdenId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineaOrdenId { get; set; }

        /// <summary>
        /// Programa Navigation
        /// </summary>
        public int ProgramaId { get; set; }

        [ForeignKey("ProgramaId")]
        public Programa Programa { get; set; }

        /// <summary>
        /// Material Navigation
        /// </summary>
        public int MaterialId { get; set; }

        [ForeignKey("MaterialId")]
        public Material Material { get; set; }

        /// <summary>
        /// Mes Navigation
        /// </summary>
        public int MesId { get; set; }

        [ForeignKey("MesId")]
        public Mes Mes { get; set; }

        public decimal TotalLinea { get; set; }
    }
}
