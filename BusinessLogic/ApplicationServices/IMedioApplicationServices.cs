using DataModule.Entities;
using Publinter.Models;
using DataModule.EntitiesResult;
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
        List<Get_Medio_Data> GetMediosSelect2Ajax(int medioId, int start, string search, int length);

        IList<Get_Medio_Data> GetMedios();
    }
}
