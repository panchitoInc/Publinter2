using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModule.Entities
{
    public class Programa
    {
        [Key, Column("ProgramaId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgramaId { get; set; }

        public string Nombre { get; set; }

        public DateTime? HoraInicio { get; set; }

        public int? Duracion { get; set; }

        public decimal PrecioSegundo { get; set; }
    }
}
