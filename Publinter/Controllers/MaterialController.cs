using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesoDatos;
using AccesoDatos.Repository;
using BusinessLogic.ApplicationServices;
using DataModule.Entities;
using Mvc;

namespace Publinter.Controllers
{
    public class MaterialController : PublinteController
    {
        IMaterialApplicationService _materialApplicationService;


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

        // GET: Material
        public ActionResult Index()
        {
            IList<Material> model = materialApplicationService.GetAll();
            return View(model);
        }

        // GET: Material/Create
        public ActionResult Create()
        {
            var tipos = materialApplicationService.GetTipos();
            ViewBag.TiposMaterial = tipos.Select(x => new SelectListItem() { Value = x.TipoMaterialId.ToString(), Text = x.Descripcion });

            Material model = new Material();

            return View(model);
        }

        // POST: Material/Create
        [HttpPost]
        public ActionResult Create(Material mat)
        {
            try
            {
                materialApplicationService.Create(mat);

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                var tipos = materialApplicationService.GetTipos();
                ViewBag.TiposMaterial = tipos.Select(x => new SelectListItem() { Value = x.TipoMaterialId.ToString(), Text = x.Descripcion });

                return View();
            }
        }

        // GET: Material/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Material/Edit/5
        [HttpPost]
        public ActionResult Edit(Material mat)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
