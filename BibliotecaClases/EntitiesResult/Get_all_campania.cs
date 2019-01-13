using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModule.EntitiesResult
{
    public class Get_all_campania
    {
        public int CampaniaId { get; set; }
        public string Nombre { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public int AnuncianteId { get; set; }
        public string AnuncianteNombre { get; set; }
    }
}
