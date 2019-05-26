using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModule.Entities
{
    public class Email:DbSet
    {
        [Key, Column("EmailId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public string Texto { get; set; }
        public string Asunto { get; set; }
        public int OrdenId { get; set; }
        public DateTime FechaEnviado { get; set; }
        [ForeignKey("OrdenId")]
        public Orden Orden { get; set; }

    }
}
