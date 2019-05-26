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

        public virtual List<LineaInternaOrden> LineasInternasOrden { get; set; }

        public decimal TotalLinea { get; set; }

        public bool Bonificada { get; set; }

        [NotMapped]
        public decimal TotalLineaSegundos { get; set; }

        public LineaOrden()
        {
            this.LineasInternasOrden = new List<LineaInternaOrden>();
        }
    }
}
