﻿using DataModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public interface IMedioApplicationServices
    {
        List<Medio> GetAll();

        int Add(Medio model);

        Medio Get(int id);
    }
}
