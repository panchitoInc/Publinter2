using BusinessLogic.ApplicationServices;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;
using Publinter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Publinter.Controllers
{
    public class CampaniaController : PublinteController
    {
        ICampaniaApplicationService _campaniaApplicationService;
        IClienteApplicationService _clienteApplicationService;
        IAnuncianteApplicationService _anuncianteApplicationService;
        IMaterialApplicationService _materialApplicationService;

        private ICampaniaApplicationService campaniaApplicationService
        {
            get
            {
                if (this._campaniaApplicationService == null)
                {
                    this._campaniaApplicationService = new CampaniaApplicationService(CurrentUser);
                }
                return this._campaniaApplicationService;
            }
        }
        private IClienteApplicationService clienteApplicationService
        {
            get
            {
                if (this._clienteApplicationService == null)
                {
                    this._clienteApplicationService = new ClienteApplicationService(CurrentUser);
                }
                return this._clienteApplicationService;
            }
        }
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
        private IMaterialApplicationService materialApplicationService
        {
            get
            {
                if (this._materialApplicationService == null)
                {
                    this._materialApplicationService = new MaterialApplicationService(CurrentUser);
                }
                return this._materialApplicationService;
            }
        }

        // GET: Campania
        public ActionResult Index()
        {
            var lista = campaniaApplicationService.GetAll();
            return View(lista);
        }

        public ActionResult Create()
        {
            CampaniaModel model = new CampaniaModel();
            model.ListaClientes = clienteApplicationService.GetClientes();
            model.ListaDeAnunciante = anuncianteApplicationService.GetAnunciantes().ToList();
            model.Materiales = new List<Material>();
            var tipos = materialApplicationService.GetTipos();
            ViewBag.TiposMaterial = tipos.Select(x => new SelectListItem() { Value = x.TipoMaterialId.ToString(), Text = x.Descripcion });
            for (var i = 0; i < 3; i++)
            {
                Material material = new Material();
                model.Materiales.Add(material);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Campania model)
        {
            try {
                model.Materiales = model.Materiales.Where(x => x.Titulo != null && x.Titulo != "").ToList();
                campaniaApplicationService.Add(model);
                return Json(true, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {

                return View();
            }
        }

        public JsonResult AddMaterialRenglon(Campania model)
        {
            var tipos = materialApplicationService.GetTipos();
            ViewBag.TiposMaterial = tipos.Select(x => new SelectListItem() { Value = x.TipoMaterialId.ToString(), Text = x.Descripcion });
            ViewData["indexMaterial"] = model.Materiales != null && model.Materiales.Count > 0 ? model.Materiales.Count : 0;
            var id = model.Materiales.Count;
            var html = RenderPartialViewToString("~/Views/Campania/Create/_create_renglon_material.cshtml", null);
            return Json(new { html, id},JsonRequestBehavior.AllowGet);
        }


        
        public ActionResult Edit(int id)
        {
            try
            {
                Campania camp = campaniaApplicationService.Get(id);
                CampaniaModel model = new CampaniaModel();
                model.CampaniaId = camp.CampaniaId;
                model.Nombre = camp.Nombre;
                model.Cliente = camp.Cliente;
                model.ClienteId = camp.ClienteId;
                model.Anunciante = camp.Anunciante;
                model.AnuncianteId = camp.AnuncianteId;
                
                model.ListaClientes = clienteApplicationService.GetClientes();
                model.ListaDeAnunciante = anuncianteApplicationService.GetAnunciantes().ToList();
                model.Materiales = camp.Materiales;
                var tipos = materialApplicationService.GetTipos();
                ViewBag.TiposMaterial = tipos.Select(x => new SelectListItem() { Value = x.TipoMaterialId.ToString(), Text = x.Descripcion });
                return View(model);
            }
            catch (Exception)
            {
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Campania model)
        {
            campaniaApplicationService.Edit(model);
            CampaniaModel modelEditado = new CampaniaModel();
            modelEditado.CampaniaId = model.CampaniaId;
            modelEditado.Nombre = model.Nombre;
            modelEditado.Cliente = model.Cliente;
            modelEditado.ClienteId = model.ClienteId;
            modelEditado.Anunciante = model.Anunciante;
            modelEditado.AnuncianteId = model.AnuncianteId;

            modelEditado.ListaClientes = clienteApplicationService.GetClientes();
            modelEditado.ListaDeAnunciante = anuncianteApplicationService.GetAnunciantes().ToList();
            modelEditado.Materiales = model.Materiales.Where(x => x.Delete.Equals(false)).ToList();
            var tipos = materialApplicationService.GetTipos();
            ViewBag.TiposMaterial = tipos.Select(x => new SelectListItem() { Value = x.TipoMaterialId.ToString(), Text = x.Descripcion });
            return View(modelEditado);
        }


        [HttpPost]
        public JsonResult GetMateriales(int campaniaId)
        {
            var materiales = campaniaApplicationService.GetMaterialesByCampania(campaniaId);
            string html = "";

            if (materiales != null && materiales.Count > 0)
            {
                foreach (Get_Material_Data m in materiales)
                {
                    html += "<option value='" + m.MaterialId + "' data-duracion='" + m.Duracion + "'>" + m.Nombre + "</option>";
                }
            }
            else
            {
                html += "<option value='0' data-duracion='0'>No hay materiales</option>";
            }

            return Json(new { html }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOptionsCampanias(int anuncianteId)
        {
            string options = "<option value='0'>Todas</option>";

            if (anuncianteId > 0)
            {
                IList<Campania> lista = campaniaApplicationService.GetCampaniasXAnunciante(anuncianteId);

                foreach (var item in lista)
                {
                    options += "<option value='" + item.CampaniaId + "'>" + item.Nombre + "</option>";
                }
            }

            return Json(new { value = true, options }, JsonRequestBehavior.AllowGet);
        }

    }
}