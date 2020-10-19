using DataModule.Entities;
using DataModule.EntitiesResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public interface IAnuncianteRepository
    {
        Anunciante Get(int id);

        List<Get_Anunciante_Data> GetAnunciantes();

        int Add(Anunciante model);

        bool Update(Anunciante model);
        Anunciante GetByRut(string rut);
        /// <summary>
        /// retorna lista anuncuiante para select2 ajax Anunciante
        /// </summary>
        /// <param name="anuncianteId"></param>
        /// <param name="start"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        List<Get_Anunciante_Data> GetAnuncuantesSelect2Ajax(int anuncianteId, int start, int length, string search);

    }
}
