﻿using DataModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Publinter.Models
{
    public class Add_Linea_Model
    {
        public Add_Linea_Model()
        {
            this.IndexLinea = 0;
            this.Lineas = new List<LineaOrden>();
        }

        public int IndexLinea { get; set; }

        public List<LineaOrden> Lineas { get; set; }
    }
}