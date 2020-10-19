using DataModule.Entities;
using DataModule.EntitiesResult;
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
        Medio GetByNameAndType(string nombre, int tipoMedio);
        int Add(Medio model);
        bool Update(Medio model);
        IList<Get_Medio_Data> GetMedios();
    }
}
