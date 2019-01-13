using DataModule;
using DataModule.EntitiesResult;
using Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public class ProgramaRepository : PublinterRepository, IProgramaRepository
    {
        public ProgramaRepository(AppUser Context) : base(Context)
        {

        }

        public List<Get_Programa_Data> GetProgramas()
        {
            List<Get_Programa_Data> programas;

            using (var context = new PublinterContext())
            {
                var _usuId = new SqlParameter("@UsarioId", this.Context.Id);
                programas = context.Database.SqlQuery<Get_Programa_Data>("GET_PROGRAMAS @UsarioId", _usuId).ToList();
            }

            return programas;
        }

        public List<Get_Programa_Data> GetProgramasByMedio(int medioId)
        {
            List<Get_Programa_Data> programas;
            using (var context = new PublinterContext())
            {
                var _mId = new SqlParameter("@MedioId", medioId);
                programas = context.Database.SqlQuery<Get_Programa_Data>("GET_PROGRAMAS_BY_MEDIO @MedioId", _mId).ToList();
            }
            return programas;
        }
    }
}
