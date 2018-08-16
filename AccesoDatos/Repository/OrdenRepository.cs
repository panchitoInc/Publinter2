using DataModule;
using DataModule.Entities;
using Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public class OrdenRepository: PublinterRepository, IOrdenRepository
    {
        public OrdenRepository(AppUser Context) : base(Context)
        {

        }

        public int GetNroOrden()
        {
            int ultimo = 0;
            //GET_LAST_COMPROBANTE

            using (var context = new PublinterContext())
            {
                var _usuId = new SqlParameter("@UsarioId", this.Context.Id);
                ultimo = context.Database.SqlQuery<int>("GET_NRO_ULTIMA_ORDEN @UsarioId", _usuId).FirstOrDefault();
            }

            return ultimo + 1;
        }

        public int CrearOrden(Orden nueva)
        {
            try
            {
                using (var context = new PublinterContext())
                {
                    context.Orden.Add(nueva);
                    context.SaveChanges();
                    return nueva.OrdenId;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
