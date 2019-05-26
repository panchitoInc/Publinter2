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
    public class ConfiguracionEmail:DbSet
    {
        [Key, Column("ConfiguracionEmailId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfiguracionEmailId { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
