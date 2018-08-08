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
    public class UsuarioAccess : DbSet
    {
        public UsuarioAccess()
        {
            this.Fecha = DateTime.Now;
            this.Expired = DateTime.Now.AddHours(1);
            this.Ip = "";
            this.Token = "";
            this.RefreshToken = "";
        }

        [Key, Column("UsuarioAccessId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string User { get; set; }

        public string Ip { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime Expired { get; set; }

        /// <summary>
        /// 1: Access ok.
        /// 2: User o pass invalid.
        /// 3: Access ok, app not authorized.
        /// </summary>
        public int TypeAccess { get; set; }

        public string Msg { get; set; }

        public int? UsuarioId { get; set; }

        public Guid? UsuarioGuid { get; set; }
    }
}
