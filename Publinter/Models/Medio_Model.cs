using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Publinter.Models
{
    public class Medio_Model
    {
        public int MedioId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int TotalRows { get; set; }
    }
}