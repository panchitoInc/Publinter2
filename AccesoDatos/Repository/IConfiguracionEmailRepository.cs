﻿using DataModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public interface IConfiguracionEmailRepository
    {
        ConfiguracionEmail Get();
        bool Update(ConfiguracionEmail model);
    }
}