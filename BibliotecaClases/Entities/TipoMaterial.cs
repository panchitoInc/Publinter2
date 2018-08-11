using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModule.Entities
{
    public class TipoMaterial
    {
        [Key, Column("TipoMaterialId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoMaterialId { get; set; }

        public string Descripcion { get; set; }
    }
}
