using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.ApplicationServices;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;

namespace Publinter.Controllers
{
    public class AnuncianteController : PublinteController
    {
        IAnuncianteApplicationService _anuncianteApplicationService;

        private IAnuncianteApplicationService anuncianteApplicationService
        {
            get
            {
                if (this._anuncianteApplicationService == null)
                {
                    this._anuncianteApplicationService = new AnuncianteApplicationService(CurrentUser);
                }
                return this._anuncianteApplicationService;
            }
        }

        public IList<Get_Anunciante_Data> GetAnunciantes()
        {
            return anuncianteApplicationService.GetAnunciantes();
        }

        public JsonResult GetMateriales(int anuncianteId)
        {
            Anunciante a = anuncianteApplicationService.Get(anuncianteId);

            string html = "";

            if (a.Materiales != null && a.Materiales.Count > 0)
            {
                foreach (Material m in a.Materiales)
                {
                    html += "<option value='" + m.MaterialId + "' data-duracion='" + m.DuracionSegundos + "'>" + m.Titulo + "</option>";
                }
            }
            else
            {
                html += "<option value='0' data-duracion='0'>No hay materiales</option>";
            }

            return Json(new { html }, JsonRequestBehavior.AllowGet);
        }
    }
}