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
    public class TipoMaterialController : PublinteController
    {
        // GET: TipoMaterial
        public ActionResult Index()
        {
            ITipoMaterialAppliactionService tipoMaterialAppliactionService = new TipoMaterialAppliactionService(CurrentUser);
            List<TipoMaterial> model = tipoMaterialAppliactionService.GetAll();
            return View(model);
        }

        [HttpPost]
        public JsonResult Create(TipoMaterial model)
        {
            bool isCreate = false;
            try
            {
                ITipoMaterialAppliactionService tipoMaterialAppliactionService = new TipoMaterialAppliactionService(CurrentUser);
                isCreate = tipoMaterialAppliactionService.Create(model);
            }
            catch(Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
            return Json(isCreate, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public JsonResult DeshabilitarTipoMaterial(int id)
        {
            ITipoMaterialAppliactionService tipoMaterialAppliactionService = new TipoMaterialAppliactionService(CurrentUser);

            var isDesHabilitado = tipoMaterialAppliactionService.DeshabilitarTipoMaterial(id);

            return Json(isDesHabilitado, JsonRequestBehavior.AllowGet);
        }

    }
    
}