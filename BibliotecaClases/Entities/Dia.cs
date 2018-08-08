using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModule.Entities
{
    public class Dia
    {
        [Key, Column("OrdenId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiaId { get; set; }

        public int DiaNumero { get; set; }

        public string DiaNombre { get; set; }

        public int NroEmisiones { get; set; }

        public decimal TotalDia { get; set; }
    }
}
