using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BibliotecaClases.Entities
{
    public class Medio
    {
        [Key, Column("MedioId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedioId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public virtual List<Contacto> Contactos { get; set; }

        public virtual List<Programa> Programas { get; set; }
    }
}
