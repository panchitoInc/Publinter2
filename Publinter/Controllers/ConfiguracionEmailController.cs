using BusinessLogic.ApplicationServices;
using DataModule.Entities;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Publinter.Controllers
{
    public class ConfiguracionEmailController : PublinteController
    {
        // GET: Configuracion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Update()
        {
            IConfiguracionEmailApplicationService configuracionApplicationService = new ConfiguracionEmailApplicationService(CurrentUser);
            ConfiguracionEmail confi = configuracionApplicationService.Get();
            return View(confi);
        }

        [HttpPost]
        public ActionResult Update(ConfiguracionEmail model)
        {
            try
            {
                IConfiguracionEmailApplicationService configuracionApplicationService = new ConfiguracionEmailApplicationService(CurrentUser);
                bool IsUpdate = configuracionApplicationService.Update(model);

                return View(model);
            }
            catch
            {
                return View(model);
            }
            
        }
    }
}