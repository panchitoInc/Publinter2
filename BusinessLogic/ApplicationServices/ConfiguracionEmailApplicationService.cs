using AccesoDatos.Repository;
using DataModule;
using DataModule.Entities;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApplicationServices
{
    public class ConfiguracionEmailApplicationService: PublinterApplicationService, IConfiguracionEmailApplicationService
    {
        public ConfiguracionEmailApplicationService(AppUser context) : base(context)
        {

        }
        public ConfiguracionEmail Get()
        {
            IConfiguracionEmailRepository configuracionApplicationService = new ConfiguracionEmailRepository(this.Context);
            return configuracionApplicationService.Get();
        }

       public bool Update(ConfiguracionEmail model)
        {
            IConfiguracionEmailRepository configuracionApplicationService = new ConfiguracionEmailRepository(this.Context);

            return configuracionApplicationService.Update(model);
        }
    }
}
