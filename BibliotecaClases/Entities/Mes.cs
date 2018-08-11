using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModule.Entities
{
    public class Mes
    {
        [Key, Column("MesId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MesId { get; set; }

        public int MesNumero { get; set; }

        public string MesNombre { get; set; }

        public virtual List<Dia> Dias { get; set; }
    }
}
