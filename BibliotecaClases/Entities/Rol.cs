using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModule.Entities
{
    public class Rol
    {
        [Key, Column("RolId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolId { get; set; }

        public string Descripcion { get; set; }
    }
}
