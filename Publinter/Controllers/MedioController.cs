using AccesoDatos;
using AccesoDatos.Repository;
using DataModule.Entities;
using BusinessLogic.ApplicationServices;
using Mvc;
using System.Web.Mvc;

namespace Publinter.Controllers
{
    public class MedioController : PublinteController    {

        IMedioApplicationServices _medioApplicationService;

        private IMedioApplicationServices medioApplicationService
        {
            get
            {
                if (this._medioApplicationService == null)
                {
                    this._medioApplicationService = new MedioApplicationServices(CurrentUser);
                }
                return this._medioApplicationService;
            }
        }

        // GET: Medio
        public ActionResult Index()
        {
            ViewBag.ListaMedios = medioApplicationService.GetAll();

            return View();
        }

        public JsonResult AddMedioRenglon(Medio medio)
        {
            var html=string.Empty;
            ViewData["indexContacto"] = medio.Contactos.Count - 1;//indica el index del ultimo obj agregado.
            html = RenderPartialViewToString("~/Views/Shared/Contacto/_medio_contacto_renglon.cshtml",null);
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}