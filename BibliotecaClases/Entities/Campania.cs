using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModule.Entities
{
    public class Campania
    {

        public Campania()
        {
            this.Materiales = new List<Material>();
        }

        [Key, Column("CampaniaId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CampaniaId { get; set; }

        public string Nombre { get; set; }
        
        /// <summary>
        /// Anunciante Navigation
        /// </summary>
        public int AnuncianteId { get; set; }

        [ForeignKey("AnuncianteId")]
        public Anunciante Anunciante { get; set; }

        /// <summary>
        /// Cliente Navigation
        /// </summary>
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        public List<Material> Materiales { get; set; }

        //public int MedioId { get; set; }

        //[ForeignKey("MedioId")]
        //public Medio Medio { get; set; }

        
    }
}
