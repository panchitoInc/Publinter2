using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModule.Entities
{
    public class Medio
    {
        public Medio()
        {
            this.Nombre = "";
            this.Descripcion = "";
            this.Programas = new List<Programa>();
            this.Contactos = new List<Contacto>();
        }

        [Key, Column("MedioId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedioId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public virtual List<Contacto> Contactos { get; set; }

        public virtual List<Programa> Programas { get; set; }


        //TipoMedio navigation
        public int? TipoMedioId { get; set; }

        [ForeignKey("TipoMedioId")]
        public virtual TipoMedio TipoMedio { get; set; }
        public decimal PorcentajeDescuento { get; set; }
    }
}
