using AccesoDatos;
using AccesoDatos.Repository;
using BibliotecaClases.Entities;
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

        //public JsonResult Create(Medio model)
        //{

        //}
    }
}