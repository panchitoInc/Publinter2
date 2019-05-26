using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModule.Entities
{
    public class LineaBonificada
    {
        [Key, Column("LineaBonificadaId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineaBonificadaId { get; set; }

        /// <summary>
        /// Mes Navigation
        /// </summary>
        public int MesId { get; set; }

        [ForeignKey("MesId")]
        public Mes Mes { get; set; }

        public int CantidadTotalBonificada { get; set; }
    }
}
