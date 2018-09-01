using DataModule.EntitiesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Publinter.Models
{
    public class DataTableDataOrden
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IList<Get_orden_index> data { get; set; }
    }
}