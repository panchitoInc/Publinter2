﻿using DataModule.Entities;
using Publinter.Models;
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

        bool Update(Medio model);
        IList<string> GetEmailsPorMedio(int medioId);
        List<Medio_Model> GetMediosSelect2Ajax(int medioId, int start, string search, int length);

    }
}
