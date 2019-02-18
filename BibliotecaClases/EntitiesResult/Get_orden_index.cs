using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModule.EntitiesResult
{
    public class Get_orden_index
    {
        public int OrdenId { get; set; }

        public string Descripcion { get; set; }

        public string Campania { get; set; }

        public decimal Total { get; set; }

        public DateTime Emision { get; set; }

        public int TotalRows { get; set; }

        public string EmisionString
        {
            get { return this.Emision.ToString("dd/MM/yyyy"); }
        }
    }
}
