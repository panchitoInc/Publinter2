﻿using BibliotecaClases.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public interface IMedioRepository
    {
        List<Medio> GetAll();
        Medio Get(int id);
    }
}
