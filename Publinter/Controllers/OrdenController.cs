using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLogic.ApplicationServices;
using DataModule.Entities;
using DataModule.EntitiesResult;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using iTextSharp.tool.xml;
using Mvc;
using Publinter.Models;

namespace Publinter.Controllers
{
    public class OrdenController : PublinteController
    {
        IOrdenApplicationService _ordenApplicationService;
        IMedioApplicationServices _medioApplicationService;
        IMaterialApplicationService _materialApplicationService;
        IClienteApplicationService _clienteApplicationService;
        IProgramaApplicationService _programaApplicationService;
        ICampaniaApplicationService _campaniaAplicationService;

        private ICampaniaApplicationService campaniaAplicationService
        {
            get
            {
                if (this._campaniaAplicationService == null)
                {
                    this._campaniaAplicationService = new CampaniaApplicationService(CurrentUser);
                }
                return this._campaniaAplicationService;
            }
        }
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

        private IProgramaApplicationService programaApplicationService
        {
            get
            {
                if (this._programaApplicationService == null)
                {
                    this._programaApplicationService = new ProgramaApplicationService(CurrentUser);
                }
                return this._programaApplicationService;
            }
        }

        public ActionResult Create()
        {
            Orden_Create_Model model = new Orden_Create_Model();
            model.ListaCampanias = campaniaAplicationService.GetAll();

            if (model.ListaCampanias.Count > 0)
            {
                var PrimerCampania = model.ListaCampanias.FirstOrDefault();
                var CampaniaConDependencias = campaniaAplicationService.Get(PrimerCampania.CampaniaId);
                model.ListaMateriales = CampaniaConDependencias.Materiales;
            }

            model.ListaMedios = medioApplicationService.GetAll();
            model.ListaProgramas = programaApplicationService.GetProgramasByMedio(model.ListaMedios.FirstOrDefault().MedioId);
            model.ListaClientes = clienteApplicationService.GetClientes();

            model.NroOrden = ordenApplicationService.GetNroOrden();
            model.UsuarioId = CurrentUser.Id;
           
            return View(model);
        }

        public ActionResult CopiarOrden(int ordenId)
        {
            Orden_Create_Model model = new Orden_Create_Model();
            model.ListaCampanias = campaniaAplicationService.GetAll();

            if (model.ListaCampanias.Count > 0)
            {
                var PrimerCampania = model.ListaCampanias.FirstOrDefault();
                var CampaniaConDependencias = campaniaAplicationService.Get(PrimerCampania.CampaniaId);
                model.ListaMateriales = CampaniaConDependencias.Materiales;
            }

            model.ListaMedios = medioApplicationService.GetAll();
            model.ListaProgramas = programaApplicationService.GetProgramasByMedio(model.ListaMedios.FirstOrDefault().MedioId);
            model.ListaClientes = clienteApplicationService.GetClientes();

            model.NroOrden = ordenApplicationService.GetNroOrden();
            model.UsuarioId = CurrentUser.Id;

            Orden aCopiar = ordenApplicationService.Get(ordenId);

            model.CampaniaId = aCopiar.CampaniaId;
            model.AnuncianteId = aCopiar.Campania.AnuncianteId;
            model.MedioId = aCopiar.MedioId;

            model.Lineas = new List<LineaOrden>();

            foreach (LineaOrden lo in aCopiar.LineasOrden)
            {
                LineaOrden nueva = new LineaOrden();
                nueva.Bonificada = lo.Bonificada;
                nueva.TotalLinea = lo.TotalLinea;
                nueva.LineasInternasOrden = new List<LineaInternaOrden>();

                foreach (LineaInternaOrden lio in lo.LineasInternasOrden)
                {
                    LineaInternaOrden nueva_interna = new LineaInternaOrden();
                    nueva_interna.MaterialId = lio.MaterialId;
                    nueva_interna.Mes = new Mes();
                    nueva_interna.Mes.MesNombre = lio.Mes.MesNombre;
                    nueva_interna.Mes.MesNumero = lio.Mes.MesNumero;
                    nueva_interna.Mes.MesAnio = lio.Mes.MesAnio;

                    nueva_interna.Mes.Dias = new List<Dia>();

                    foreach (Dia d in lio.Mes.Dias)
                    {
                        Dia nuevo = new Dia();
                        nuevo.DiaNombre = d.DiaNombre;
                        nuevo.DiaNumero = d.DiaNumero;
                        nuevo.TotalDia = d.TotalDia;
                        nuevo.NroEmisiones = d.NroEmisiones;

                        nueva_interna.Mes.Dias.Add(d);
                    }

                    if (lio.LineaBonificadaId.HasValue)
                    {
                        nueva_interna.LineaBonificada = new LineaBonificada();
                        nueva_interna.LineaBonificada.CantidadTotalBonificada = lio.LineaBonificada.CantidadTotalBonificada;
                        nueva_interna.LineaBonificada.Mes = new Mes();
                        nueva_interna.LineaBonificada.Mes.MesNombre = lio.LineaBonificada.Mes.MesNombre;
                        nueva_interna.LineaBonificada.Mes.MesNumero = lio.LineaBonificada.Mes.MesNumero;
                        nueva_interna.LineaBonificada.Mes.MesAnio = lio.LineaBonificada.Mes.MesAnio;
                        nueva_interna.LineaBonificada.Mes.Dias = new List<Dia>();

                        foreach (Dia d in lio.Mes.Dias)
                        {
                            Dia nuevo = new Dia();
                            nuevo.DiaNombre = d.DiaNombre;
                            nuevo.DiaNumero = d.DiaNumero;
                            nuevo.TotalDia = d.TotalDia;
                            nuevo.NroEmisiones = d.NroEmisiones;

                            nueva_interna.LineaBonificada.Mes.Dias.Add(d);
                        }
                    }

                    nueva.LineasInternasOrden.Add(nueva_interna);
                }

                model.Lineas.Add(nueva);
            }

            model.TotalOrden = aCopiar.Total;

            return View("Create", model);
        }

        [HttpPost]
        public JsonResult Create(Orden_Create_Model model)
        {
            bool value;

            try
            {
                Orden nueva = model.ToOrden();
                ordenApplicationService.CrearOrden(nueva);

                if(model.GuardarEnviarDescargar == 1)
                {
                    createPdf(model);
                }
                else if(model.GuardarEnviarDescargar == 2)
                {
                    SendEmailWithPdf(model);
                }
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

        [HttpPost]
        public ActionResult AjaxGetDataOrdenIndex(int draw, int start, int length)
        {
            int TOTAL_ROWS = 0;
            string search = Request["search[value]"];
            int sortColumn = 0;
            string sortDirection = "asc";

            if (Request["order[0][column]"] != null)
            {
                sortColumn = int.Parse(Request["order[0][column]"]);
            }
            if (Request["order[0][dir]"] != null)
            {
                sortDirection = Request["order[0][dir]"];
            }

            DataTableDataOrden dataTableData = new DataTableDataOrden();

            dataTableData.draw = draw;

            var ordenes = ordenApplicationService.GetIndex(start, length, sortColumn, sortDirection, search).ToList();

            if (ordenes != null && ordenes.Count > 0)
            {
                TOTAL_ROWS = ordenes[0].TotalRows;
            }
            else
            {
                TOTAL_ROWS = 0;
            }
            dataTableData.recordsTotal = TOTAL_ROWS;

            dataTableData.data = ordenes;
            dataTableData.recordsFiltered = TOTAL_ROWS;

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrdenesSelect(int campaniaId, int medioId)
        {
            List<Get_Orden_Select> ordenes = ordenApplicationService.GetOrdenesSelect(campaniaId, medioId);

            var html = "<option value='0'>Seleccione orden</orden>";

            foreach (Get_Orden_Select item in ordenes)
            {
                html += "<option value='" + item.OrdenId + "'>" + item.Descripcion + "</option>";
            }

            return Json(html, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarPrimeraLinea(Orden_Create_Model model)
        {
            Add_Linea_Model viewModel = new Add_Linea_Model();

            int cantLineas = 0;

            viewModel.IndexLinea = cantLineas;

            LineaOrden nueva = new LineaOrden();
            nueva.LineasInternasOrden = new List<LineaInternaOrden>();
            nueva.Bonificada = false;

            LineaInternaOrden primera = new LineaInternaOrden();

            Mes mesActual = new Mes();

            mesActual.MesAnio = DateTime.Now.Year;
            mesActual.MesNumero = DateTime.Now.AddMonths(1).Month;
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

            primera.Mes = mesActual;

            primera.LineaBonificada = new LineaBonificada();
            primera.LineaBonificada.Mes = mesActual;

            nueva.LineasInternasOrden.Add(primera);

            viewModel.Lineas.Add(nueva);

            viewModel.ListaMateriales = materialApplicationService.GetAll().ToList();
            viewModel.ListaProgramas = programaApplicationService.GetProgramas();

            string html = RenderPartialViewToString("AddLinea", viewModel);
            int index = 0;
            bool value = true;

            return Json(new { value, html, index }, JsonRequestBehavior.AllowGet);
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

            nueva.LineasInternasOrden = new List<LineaInternaOrden>();
            nueva.Bonificada = false;

            LineaInternaOrden primera = new LineaInternaOrden();

            Mes mesActual = new Mes();

            mesActual.MesAnio = model.Lineas[cantLineas - 1].LineasInternasOrden[0].Mes.MesAnio;
            mesActual.MesNumero = model.Lineas[cantLineas - 1].LineasInternasOrden[0].Mes.MesNumero + 1;
            if (mesActual.MesNumero > 12)
            {
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

            primera.Mes = mesActual;

            primera.LineaBonificada = new LineaBonificada();
            primera.LineaBonificada.Mes = mesActual;

            nueva.LineasInternasOrden.Add(primera);

            viewModel.Lineas.Add(nueva);

            //viewModel.ListaMateriales = materialApplicationService.GetAll().ToList();
            ///viewModel.ListaProgramas = programaApplicationService.GetProgramas();

            var CampaniaConDependencias = campaniaAplicationService.Get(model.CampaniaId);
            viewModel.ListaMateriales = CampaniaConDependencias.Materiales;
            viewModel.ListaProgramas = programaApplicationService.GetProgramasByMedio(model.MedioId);

            string html = RenderPartialViewToString("AddLinea", viewModel);
            int index = viewModel.Lineas.Count - 1;
            bool value = true;

            return Json(new { value, html, index }, JsonRequestBehavior.AllowGet);
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

            int cantLineasInternas = model.Lineas[cantLineas - 1].LineasInternasOrden.Count;

            LineaOrden nueva = new LineaOrden();
            nueva.Bonificada = false;

            for (int i = 0; i < cantLineasInternas; i++)
            {
                Mes mesActual = new Mes();

                LineaInternaOrden interna = new LineaInternaOrden();

                model.Lineas[cantLineas - 1].LineasInternasOrden.Add(new LineaInternaOrden());

                mesActual.MesAnio = model.Lineas[cantLineas - 1].LineasInternasOrden[i].Mes.MesAnio;
                mesActual.MesNumero = model.Lineas[cantLineas - 1].LineasInternasOrden[i].Mes.MesNumero;

                mesActual.MesNombre = model.Lineas[cantLineas - 1].LineasInternasOrden[i].Mes.MesNombre;

                mesActual.Dias = new List<Dia>();

                foreach (Dia d in model.Lineas[cantLineas - 1].LineasInternasOrden[i].Mes.Dias)
                {
                    Dia dia = new Dia();
                    dia.DiaNumero = d.DiaNumero;
                    dia.DiaNombre = d.DiaNombre;
                    dia.NroEmisiones = d.NroEmisiones;
                    dia.TotalDia = d.TotalDia;

                    mesActual.Dias.Add(dia);
                }

                interna.Mes = mesActual;

                if (model.Lineas[cantLineas - 1].LineasInternasOrden[i].LineaBonificadaId.HasValue)
                {
                    interna.LineaBonificada = new LineaBonificada();
                    interna.LineaBonificada.CantidadTotalBonificada = model.Lineas[cantLineas - 1].LineasInternasOrden[i].LineaBonificada.CantidadTotalBonificada;

                    interna.LineaBonificada.Mes = new Mes();
                    interna.LineaBonificada.Mes.MesAnio = model.Lineas[cantLineas - 1].LineasInternasOrden[i].LineaBonificada.Mes.MesAnio;
                    interna.LineaBonificada.Mes.MesNumero = model.Lineas[cantLineas - 1].LineasInternasOrden[i].LineaBonificada.Mes.MesNumero;
                    interna.LineaBonificada.Mes.MesNombre = model.Lineas[cantLineas - 1].LineasInternasOrden[i].LineaBonificada.Mes.MesNombre;

                    interna.LineaBonificada.Mes.Dias = new List<Dia>();

                    foreach (Dia d in model.Lineas[cantLineas - 1].LineasInternasOrden[i].LineaBonificada.Mes.Dias)
                    {
                        Dia dia = new Dia();
                        dia.DiaNumero = d.DiaNumero;
                        dia.DiaNombre = d.DiaNombre;
                        dia.NroEmisiones = d.NroEmisiones;
                        dia.TotalDia = d.TotalDia;

                        interna.LineaBonificada.Mes.Dias.Add(dia);
                    }
                }

                nueva.LineasInternasOrden.Add(interna);
            }

            viewModel.Lineas.Add(nueva);


            viewModel.ListaMateriales = materialApplicationService.GetAll().ToList();
            viewModel.ListaProgramas = programaApplicationService.GetProgramas();

            string html = RenderPartialViewToString("AddLinea", viewModel);
            int index = viewModel.Lineas.Count - 1;
            bool value = true;

            return Json(new { value, html, index }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarLineaInterna(Orden_Create_Model model)
        {
            model.IndexLineaInternaParaAgregar = model.Lineas[model.IndexLineaParaAgregar].LineasInternasOrden.Count;

            LineaInternaOrden nueva = new LineaInternaOrden();
            nueva.Mes = model.Lineas[model.IndexLineaParaAgregar].LineasInternasOrden[model.IndexLineaInternaParaAgregar - 1].Mes;

            nueva.Mes.MesId = 0;
            foreach (Dia d in nueva.Mes.Dias)
            {
                d.NroEmisiones = 0;
            }

            nueva.LineaBonificada = new LineaBonificada();
            nueva.LineaBonificada.Mes = nueva.Mes;

            model.Lineas[model.IndexLineaParaAgregar].LineasInternasOrden.Add(nueva);

            model.ListaMateriales = materialApplicationService.GetAll().ToList();
            model.ListaProgramas = programaApplicationService.GetProgramas();

            string html = RenderPartialViewToString("AddLineaInterna", model);

            bool value = true;

            int indexLineaInterna = model.IndexLineaInternaParaAgregar;

            return Json(new { value, html, indexLineaInterna }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult createPdf(Orden_Create_Model model)
        {
            MemoryStream workStream = this.CreateDocumentPdf(model);
            return File(workStream, "application/pdf");
        }
        private MemoryStream CreateDocumentPdf(Orden_Create_Model model)
        {
            model.Campania = campaniaAplicationService.Get(model.CampaniaId);
            model.Medio = medioApplicationService.Get(model.MedioId);
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            document.Open();
            // setting image 
            string imagePath = "C:/Users/germa/Desktop/Publinter2/Publinter2/Publinter/Content/Images/Logo-Publinter-dark-2017-final.png";
            iTextSharp.text.Image tif = iTextSharp.text.Image.GetInstance(imagePath);
            tif.ScalePercent(24f);
            tif.SetAbsolutePosition(document.PageSize.Width - 36f - 140f,
            document.PageSize.Height - 75f);
            document.Add(tif);
            // fin imagenes
            //Titulo Orden
            iTextSharp.text.Font titleFont = FontFactory.GetFont("Garamond", 20);
            iTextSharp.text.Font regularFont = FontFactory.GetFont("Arial", 14);

            Paragraph OrdenNumero = new Paragraph("Órden Nro. " + model.NroOrden.ToString(), titleFont);
            OrdenNumero.Alignment = Element.ALIGN_LEFT;
            document.Add(OrdenNumero);
            //fin titulo orden
            //Linea separadora titulo
            Paragraph pLineaSeparadora = new Paragraph(new Chunk(new LineSeparator(0.01F, 100.0F, BaseColor.LIGHT_GRAY, Element.ALIGN_LEFT, 1)));
            //   pLineaSeparadora.ExtraParagraphSpace =30;
            document.Add(pLineaSeparadora);
            //

            CreateTableMedioCampaniaEmision(document, model);

            document.Add(pLineaSeparadora);
            //Table
            CreateTable(document, model);
            //Agrego linea footer

            document.Add(pLineaSeparadora);
            //Linea separadora footer
            Paragraph InversionTotal = new Paragraph("INVERSIÓN TOTAL: $" + model.TotalOrden.ToString());
            InversionTotal.Alignment = Element.ALIGN_CENTER;
            document.Add(InversionTotal);
            document.Close();
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Flush();
            workStream.Position = 0;
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("Orden" + model.NroOrden.ToString() + ".pdf"));
            Response.ContentType = "APPLICATION/pdf";
            Response.BinaryWrite(byteInfo);
            return workStream;
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

        private Document  CreateTable(Document document, Orden_Create_Model model)
        {
            try
            {
                //fuente
                var FontWhite = FontFactory.GetFont("Helvetica", 10, new BaseColor(Color.White));
                var FontGris = FontFactory.GetFont("Helvetica", 18, new BaseColor(Color.Gray));
                //
                float[] widths = new float[] { 80f, 80f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f, 22.6f };
                for (var i = 0; i < model.Lineas.Count; i++)
                {
                    // Ante de crear tabla iserto un parrafo para darle margen
                    Paragraph PmargenTop = new Paragraph("");
                    PmargenTop.PaddingTop = 30;
                    document.Add(PmargenTop);
                    PdfPTable table = new PdfPTable(33);
                    table.WidthPercentage = 100; //table width to 100per

                    table.SetWidths(widths);
                    // thead 2 primeras
                    PdfPCell ColSpan2 = new PdfPCell(new Phrase());
                    ColSpan2.Colspan = 2;
                    ColSpan2.BorderColor = BaseColor.WHITE;
                    table.AddCell(ColSpan2);
                    // thead titulo mes
                    Phrase ph1 = new Phrase();
                    ph1.Add(new Chunk(Environment.NewLine));
                    PdfPCell colSpan33 = new PdfPCell(new Phrase(model.Lineas[i].LineasInternasOrden[0].Mes.MesNombre, FontGris));
                    colSpan33.Colspan = 33;
                    colSpan33.Border = 0;
                    colSpan33.BorderWidth = 0;
                    colSpan33.BorderColor = new BaseColor(Color.LightGray);
                    colSpan33.BackgroundColor = new BaseColor(Color.LightGray);
                    colSpan33.HorizontalAlignment = Element.ALIGN_CENTER;
                    colSpan33.VerticalAlignment = Element.ALIGN_MIDDLE;
                    colSpan33.PaddingBottom = 10;
                    table.DefaultCell.BorderWidth = 0;
                    table.AddCell(colSpan33);
                    PdfPCell RowSpan2 = new PdfPCell(new Phrase("Ubicación", FontWhite));
                    RowSpan2.Rowspan = 2;
                    RowSpan2.PaddingTop = 8;
                    RowSpan2.HorizontalAlignment = Element.ALIGN_CENTER;
                    RowSpan2.VerticalAlignment = Element.ALIGN_CENTER;
                    RowSpan2.BackgroundColor = new BaseColor(Color.Black);
                    table.AddCell(RowSpan2);
                    RowSpan2.Phrase = new Phrase("Materiales", FontWhite);
                    RowSpan2.PaddingTop = 10;
                    table.AddCell(RowSpan2);
                    //
                    PdfPCell simpleCel = new PdfPCell();
                    simpleCel.UseVariableBorders = true;
                    simpleCel.BorderColor = BaseColor.BLACK;
                    simpleCel.HorizontalAlignment = Element.ALIGN_CENTER;
                    simpleCel.PaddingBottom = 5;
                    foreach (var dia in model.Lineas[i].LineasInternasOrden[0].Mes.Dias)
                    {
                        simpleCel.Phrase = new Phrase(dia.DiaNombre[0].ToString());//primer letra del dia.
                        simpleCel.Phrase.Font.Size = 10;
                        table.AddCell(simpleCel);
                    }
                    //Completo el mes segun los dias
                    if (model.Lineas[i].LineasInternasOrden[0].Mes.Dias.Count < 31)
                        simpleCel = new PdfPCell(new Phrase(""));
                    {
                        if (model.Lineas[i].LineasInternasOrden[0].Mes.Dias.Count == 30)
                        {

                            table.AddCell(simpleCel);
                        }
                        else if (model.Lineas[i].LineasInternasOrden[0].Mes.Dias.Count == 29)
                        {
                            table.AddCell(simpleCel);
                            table.AddCell(simpleCel);
                        }
                        if (model.Lineas[i].LineasInternasOrden[0].Mes.Dias.Count == 28)
                        {
                            table.AddCell(simpleCel);
                            table.AddCell(simpleCel);
                            table.AddCell(simpleCel);
                        }
                    }
                    //Completa con los numeros de dias
                    foreach (var dia in model.Lineas[i].LineasInternasOrden[0].Mes.Dias)
                    {
                        simpleCel.Phrase = new Phrase(dia.DiaNumero.ToString());
                        table.AddCell(simpleCel);
                    }
                    //Completo el mes segun los dias
                    if (model.Lineas[i].LineasInternasOrden[0].Mes.Dias.Count < 31)
                    {
                        simpleCel = new PdfPCell(new Phrase(""));

                        if (model.Lineas[i].LineasInternasOrden[0].Mes.Dias.Count == 30)
                        {
                            table.AddCell(simpleCel);
                        }
                        else if (model.Lineas[i].LineasInternasOrden[0].Mes.Dias.Count == 29)
                        {
                            table.AddCell(simpleCel);
                            table.AddCell(simpleCel);
                        }
                        if (model.Lineas[i].LineasInternasOrden[0].Mes.Dias.Count == 28)
                        {
                            table.AddCell(simpleCel);
                            table.AddCell(simpleCel);
                            table.AddCell(simpleCel);
                        }
                    }
                    foreach (var lineaInterna in model.Lineas[i].LineasInternasOrden)
                    {
                        //linea de inversione
                        RowSpan2 = new PdfPCell(new Phrase(model.Medio.Programas.FirstOrDefault(x => x.ProgramaId.Equals(lineaInterna.ProgramaId)).Nombre));
                        RowSpan2.Rowspan = 2;
                        RowSpan2.PaddingTop = 3;
                        RowSpan2.Phrase.Font.Size = 10;
                        RowSpan2.PaddingBottom = 3;
                        RowSpan2.HorizontalAlignment = Element.ALIGN_CENTER;
                        RowSpan2.VerticalAlignment = Element.ALIGN_CENTER;
                        RowSpan2.BackgroundColor = new iTextSharp.text.BaseColor(Color.White);
                        table.AddCell(RowSpan2);

                        RowSpan2.Phrase = new Phrase(model.Campania.Materiales.FirstOrDefault(x => x.MaterialId.Equals(lineaInterna.MaterialId)).Titulo);
                        RowSpan2.Phrase.Font.Size = 10;
                        table.AddCell(RowSpan2);
                        //
                        simpleCel = new PdfPCell();
                        simpleCel.UseVariableBorders = true;
                        simpleCel.BorderColor = BaseColor.BLACK;

                        simpleCel.PaddingTop = 6;
                        for (var d = 0; d < lineaInterna.Mes.Dias.Count; d++)
                        {
                            simpleCel.Phrase = new Phrase(lineaInterna.Mes.Dias[d].NroEmisiones.ToString());
                            simpleCel.HorizontalAlignment = Element.ALIGN_CENTER;
                            simpleCel.VerticalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(simpleCel);
                        }
                        if (lineaInterna.Mes.Dias.Count < 31)
                        {
                            simpleCel = new PdfPCell(new Phrase(""));
                            if (lineaInterna.Mes.Dias.Count == 30)
                            {
                                table.AddCell(simpleCel);
                            }
                            else if (lineaInterna.Mes.Dias.Count == 29)
                            {
                                table.AddCell(simpleCel);
                                table.AddCell(simpleCel);
                            }
                            if (lineaInterna.Mes.Dias.Count == 28)
                            {
                                table.AddCell(simpleCel);
                                table.AddCell(simpleCel);
                                table.AddCell(simpleCel);
                            }
                        }
                    }

                    //table.SpacingAfter = 5;
                    table.SpacingBefore = 8;
                    document.Add(table);
                }
            }
            catch (Exception e)
            {
                return document;
            }
            
            

            return document;
        }
        private Document CreateTableMedioCampaniaEmision(Document document, Orden_Create_Model model)
        {
          
            Phrase CampaniaPh = new Phrase("Campaña: " + model.Campania.Nombre);
            Phrase MedioPh = new Phrase("Medio: " + model.Medio.Nombre );
            Phrase EmisionPh = new Phrase("Emisión: " + model.Emision.ToString("dd/MM/yyyy"));
            PdfPTable TableMedioCampaniaEmision = new PdfPTable(3);
            TableMedioCampaniaEmision.WidthPercentage = 100;
            PdfPCell unacell = new PdfPCell(CampaniaPh);
            unacell.BorderWidth = 0;
            unacell.HorizontalAlignment = Element.ALIGN_LEFT;
            TableMedioCampaniaEmision.AddCell(unacell);
            unacell = new PdfPCell(MedioPh);
            unacell.BorderWidth = 0;
            unacell.HorizontalAlignment = Element.ALIGN_CENTER;
            TableMedioCampaniaEmision.AddCell(unacell);
            unacell = new PdfPCell(EmisionPh);
            unacell.BorderWidth = 0;
            unacell.HorizontalAlignment = Element.ALIGN_RIGHT;
            TableMedioCampaniaEmision.AddCell(unacell);
            TableMedioCampaniaEmision.SpacingBefore = 10;

            document.Add(TableMedioCampaniaEmision);
            return document;
        }
        [HttpGet]
        public ActionResult DescargarPdf(int ordenId)
        {
            Orden orden = ordenApplicationService.Get(ordenId);
            Orden_Create_Model model = new Orden_Create_Model();
            model.CampaniaId = orden.CampaniaId;
            model.Emision = orden.Emision;
            model.Lineas = orden.LineasOrden;
            model.MedioId = orden.MedioId;
            return createPdf(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmailWithPdf(Orden_Create_Model model)
        {
            
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("analiacortimorales@gmail.com"));  // replace with valid value 
                message.From = new MailAddress("hernanfaggiani@gmail.com");  // replace with valid value
                message.Subject = "Prueba";
                message.Body = string.Format(body, "Hernan","hernanfaggiani@gmail.com", "mensaje de prueba");
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "hernanfaggiani@gmail.com",  // replace with valid value
                        Password = "2442003manumanu"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Create");
                }
            
        }

    }
}