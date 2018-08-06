using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BibliotecaClases.Entities
{
    public class Material
    {
        [Key, Column("ProgramaId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialId { get; set; }

        public string Descripcion { get; set; }

        public int DuracionSegundos { get; set; }
    }
}
