using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.ApplicationServices;
using DataModule.Entities;
using Mvc;
using Publinter.Models;

namespace Publinter.Controllers
{
    public class OrdenController : PublinteController
    {
        IOrdenApplicationService _ordenApplicationService; 
        IMedioApplicationServices _medioApplicationService;
        IMaterialApplicationService _materialApplicationService;

        private IOrdenApplicationService ordenApplicationService
        {
            get
            {
                if (this._ordenApplicationService == null)
                {
                    this._ordenApplicationService = new OrdenApplicationService(CurrentUser);
                }
                return this._ordenApplicationService;
            }
        }

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

        public ActionResult Create()
        {
            Orden_Create_Model model = new Orden_Create_Model();

            model.ListaMedios = medioApplicationService.GetAll();
            model.ListaMateriales = materialApplicationService.GetAll().ToList();
            model.NroOrden = ordenApplicationService.GetNroOrden();
            model.UsuarioId = CurrentUser.Id;

            return View(model);
        } 

        [HttpPost]
        public JsonResult Create(Orden_Create_Model model)
        {
            bool value;

            try
            {
                Orden nueva = model.ToOrden();
                ordenApplicationService.CrearOrden(nueva);

                value = true;

                return Json(new { value }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                value = false;

                string errorMsg = "Error al crear la oren : " + e.Message;

                return Json(new { value, errorMsg }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Orden
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AgregarSiguienteMes(Orden_Create_Model model)
        {
            Add_Linea_Model viewModel = new Add_Linea_Model();

            int cantLineas = model.Lineas.Count;

            viewModel.IndexLinea = cantLineas;

            for (int i = 0; i < cantLineas; i++)
            {
                viewModel.Lineas.Add(new LineaOrden());
            }

            LineaOrden nueva = new LineaOrden();

            Mes mesActual = new Mes();

            mesActual.MesAnio = model.Lineas[cantLineas - 1].Mes.MesAnio;
            mesActual.MesNumero = model.Lineas[cantLineas - 1].Mes.MesNumero + 1;
            if (mesActual.MesNumero > 12) {
                mesActual.MesNumero = 1;
                mesActual.MesAnio++;
            } 

            mesActual.MesNombre = this.GetMesNombre(mesActual.MesNumero);

            mesActual.Dias = new List<Dia>();

            int nroDias = DateTime.DaysInMonth(mesActual.MesAnio, mesActual.MesNumero);

            for (int i = 0; i < nroDias; i++)
            {
                Dia dia = new Dia();
                dia.DiaNumero = i + 1;

                DateTime fecha = new DateTime();
                string fechaString = dia.DiaNumero + "/" + mesActual.MesNumero + "/" + DateTime.Now.Year;
                DateTime.TryParseExact(fechaString, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha);
                dia.DiaNombre = this.GetDiaNombre(fecha.DayOfWeek.ToString());

                dia.NroEmisiones = 0;
                dia.TotalDia = 0;

                mesActual.Dias.Add(dia);
            }

            nueva.Mes = mesActual;

            viewModel.Lineas.Add(nueva);

            string html = RenderPartialViewToString("AddLinea", viewModel);
            bool value = true;

            return Json(new { value, html }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CopiarUltimoMes(Orden_Create_Model model)
        {
            Add_Linea_Model viewModel = new Add_Linea_Model();

            int cantLineas = model.Lineas.Count;

            viewModel.IndexLinea = cantLineas;

            for (int i = 0; i < cantLineas; i++)
            {
                viewModel.Lineas.Add(new LineaOrden());
            }

            LineaOrden nueva = new LineaOrden();

            Mes mesActual = new Mes();

            mesActual.MesAnio = model.Lineas[cantLineas -1].Mes.MesAnio;
            mesActual.MesNumero = model.Lineas[cantLineas -1].Mes.MesNumero;

            mesActual.MesNombre = model.Lineas[cantLineas - 1].Mes.MesNombre;

            mesActual.Dias = new List<Dia>();
            
            foreach (Dia d in model.Lineas[cantLineas - 1].Mes.Dias)
            {
                Dia dia = new Dia();
                dia.DiaNumero = d.DiaNumero;
                dia.DiaNombre = d.DiaNombre;
                dia.NroEmisiones = d.NroEmisiones;
                dia.TotalDia = d.TotalDia;

                mesActual.Dias.Add(dia);
            }

            nueva.Mes = mesActual;

            viewModel.Lineas.Add(nueva);

            string html = RenderPartialViewToString("AddLinea", viewModel);
            bool value = true;

            return Json(new { value, html }, JsonRequestBehavior.AllowGet);
        }

        private string GetMesNombre(int mes)
        {
            string retorno = "";
            switch (mes)
            {
                case 1:
                    retorno = "Enero";
                    break;
                case 2:
                    retorno = "Febrero";
                    break;
                case 3:
                    retorno = "Marzo";
                    break;
                case 4:
                    retorno = "Abril";
                    break;
                case 5:
                    retorno = "Mayo";
                    break;
                case 6:
                    retorno = "Junio";
                    break;
                case 7:
                    retorno = "Julio";
                    break;
                case 8:
                    retorno = "Agosto";
                    break;
                case 9:
                    retorno = "Setiembre";
                    break;
                case 10:
                    retorno = "Octubre";
                    break;
                case 11:
                    retorno = "Noviembre";
                    break;
                case 12:
                    retorno = "Diciembre";
                    break;
            }

            return retorno;
        }

        private string GetDiaNombre(string dia)
        {
            string retorno = "";

            switch (dia)
            {
                case "Sunday":
                    retorno = "Domingo";
                    break;
                case "Monday":
                    retorno = "Lunes";
                    break;
                case "Tuesday":
                    retorno = "Martes";
                    break;
                case "Wednesday":
                    retorno = "Miércoles";
                    break;
                case "Thursday":
                    retorno = "Jueves";
                    break;
                case "Friday":
                    retorno = "Viernes";
                    break;
                case "Saturday":
                    retorno = "Sábado";
                    break;
            }

            return retorno;
        }
    }
}