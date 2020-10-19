using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModule.EntitiesResult
{
    public class Get_Anunciante_Data
    {
        public int AnuncianteId { get; set; }

        public string Nombre { get; set; }
        public string RazonSocial { get; set; }
        public string RUT { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int TotalRows { get; set; }
    }
}
