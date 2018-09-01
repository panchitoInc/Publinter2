using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModule.Entities
{
    public class Cliente
    {
        [Key, Column("ClienteId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteId { get; set; }

        public string Nombre { get; set; }
        
        public virtual List<Contacto> Contactos { get; set; }

        public string RazonSocial { get; set; }

        public string RUT { get; set; }

        public virtual List<Material> Materiales { get; set; }
    }
}
