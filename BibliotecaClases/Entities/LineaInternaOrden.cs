using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModule.Entities
{
    public class LineaInternaOrden
    {
        [Key, Column("LineaInternaOrdenId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineaInternaOrdenId { get; set; }

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
    }
}
