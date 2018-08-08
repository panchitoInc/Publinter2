using DataModule.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModule.Entities
{
    public class Material
    {
        [Key, Column("MaterialId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialId { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public int DuracionSegundos { get; set; }

        /// <summary>
        /// TipoMaterial Navigation
        /// </summary>
        public int TipoMaterialId { get; set; }

        [ForeignKey("TipoMaterialId")]
        public TipoMaterial TipoMaterial { get; set; }
    }
}
