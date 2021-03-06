﻿using DataModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public interface IUsuarioApplicationService
    {
        int Create(Usuario usu);
        IList<Usuario> GetAll();
        Usuario Get(int id);
        bool Update(Usuario usu);
    }

}
