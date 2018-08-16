using DataModule.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using Publinter.Extensions;
using System.Linq;
using System.Web;

namespace Publinter.Models
{
    public class Orden_Create_Model
    {
        public Orden_Create_Model()
        {
            this.Emision = DateTime.Now;
            this.ListaProgramas = new List<Programa>();
            this.ListaMateriales = new List<Material>();

            this.Lineas = new List<LineaOrden>();

            LineaOrden nueva = new LineaOrden();
       
            Mes mesActual = new Mes();

            mesActual.MesNumero = DateTime.Now.AddMonths(1).Month;
            mesActual.MesAnio = DateTime.Now.AddMonths(1).Year;
            mesActual.MesNombre = this.GetMesNombre(mesActual.MesNumero);

            mesActual.Dias = new List<Dia>();

            int nroDias = DateTime.DaysInMonth(DateTime.Now.AddMonths(1).Year, mesActual.MesNumero);

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

            this.Lineas.Add(nueva);
        }

        public int OrdenId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime Emision { get; set; }

        public int NroOrden { get; set; }

        public int MedioId { get; set; }

        public Medio Medio { get; set; }

        public List<LineaOrden> Lineas { get; set; }

        public decimal TotalOrden { get; set; }

        public List<Medio> ListaMedios { get; set; }

        public List<Programa> ListaProgramas { get; set; }

        public List<Material> ListaMateriales { get; set; }

        public Orden ToOrden()
        {
            Orden orden = new Orden();

            orden.Emision = this.Emision;
            orden.NroOrden = this.NroOrden;

            orden.MedioId = this.MedioId;
            orden.LineasOrden = this.Lineas;

            orden.Total = this.TotalOrden;

            orden.UsuarioId = this.UsuarioId;

            return orden;
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

            switch (dia) {
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