using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModule.Entities
{
    public class Usuario
    {
        [Key, Column("UsuarioId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }

        /// <summary>
        /// Rol Navigation
        /// </summary>
        public int RolId { get; set; }

        [ForeignKey("RolId")]
        public Rol Rol { get; set; }

        public string NombreUsuario { get; set; }

        public string Password { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }
        

        /// <summary>
        /// Contacto Navigation
        /// </summary>
        public int? ContactoId { get; set; }

        [ForeignKey("ContactoId")]
        public Contacto Contacto { get; set; }

      
    }
}
