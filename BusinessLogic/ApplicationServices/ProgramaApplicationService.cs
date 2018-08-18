using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Repository;
using DataModule.EntitiesResult;
using Mvc;

namespace BusinessLogic.ApplicationServices
{
    public class ProgramaApplicationService : PublinterApplicationService, IProgramaApplicationService
    {
        IProgramaRepository programaRepository;

        public ProgramaApplicationService(AppUser context) : base(context)
        {
            this.programaRepository = new ProgramaRepository(context);
        }

        public List<Get_Programa_Data> GetProgramas()
        {
            return programaRepository.GetProgramas();
        }
    }
}
