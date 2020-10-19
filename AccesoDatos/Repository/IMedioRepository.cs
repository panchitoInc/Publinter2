using DataModule.Entities;
using Publinter.Models;
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
        List<Medio_Model> GetMediosSelect2Ajax(int medioId, int start, string search, int length);
    }
}
