using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Text;

namespace DataModule.Entities
{
    public class Contacto : DbSet
    {
        [Key, Column("ContactoId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactoId { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Ciudad { get; set; }

        public string Departamento { get; set; }

        public bool  Delete { get; set; }
    }
}
