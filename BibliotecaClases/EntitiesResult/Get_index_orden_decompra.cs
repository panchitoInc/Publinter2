using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModule.EntitiesResult
{
    public class Get_index_orden_decompra
    {
        public int OrdenDeCompraId { get; set; }
        public decimal Salidas { get; set; }
        public decimal Saldo { get; set; }
        public int NumeroDeOrdenDeCompra { get; set; }
        public DateTime Emision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int MedioId { get; set; }
        public string Nombre { get; set; }
        public decimal PorcentajeDescuento { get; set; }
    }
}
