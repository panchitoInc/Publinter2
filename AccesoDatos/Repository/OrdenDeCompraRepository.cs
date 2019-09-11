using DataModule;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public class OrdenDeCompraRepository: PublinterRepository,IOrdenDeCompraRepository
    {
        public OrdenDeCompraRepository(AppUser context):base(context) { }
        public bool Add(OrdenDeCompra model)
        {
            bool salida = false;
            try
            {
                using (var context = new PublinterContext())
                {
                    context.OrdenDeCompra.Add(model);
                    context.SaveChanges();
                }
                return salida;
            }
            catch(Exception e)
            {
                return salida;
            }
            
        }
        public int GetUltimoNumeroOrdenDeCompra()
        {
            int ultimo = 0;
            using (var context = new PublinterContext())
            {
                ultimo = context.Database.SqlQuery<int>("GET_UTIMO_NUMERO_ORDEN_DECOMPRA").FirstOrDefault();
            }
            return ultimo + 1;
        }

        public List<Get_index_orden_decompra> GetAll()
        {
            List<Get_index_orden_decompra> lista = new List<Get_index_orden_decompra>();
            using (var context = new PublinterContext())
            {
                lista = context.Database.SqlQuery<Get_index_orden_decompra>("GET_INDEX_ORDEN_DECOMPRA").ToList();
            }
            return lista;
        }

        public List<Get_OrdenDeCompra_Select> GetOrdenesSelect(int medioId)
        {
            List<Get_OrdenDeCompra_Select> ListaOrdenes = new List<Get_OrdenDeCompra_Select>();

            using (var context = new PublinterContext())
            {
                var _medioid = new SqlParameter("@MEDIOID", medioId);
                ListaOrdenes = context.Database.SqlQuery<Get_OrdenDeCompra_Select>("GET_ORDENES_DE_COMPRA_SELECT @MEDIOID", _medioid).ToList();
            }

            return ListaOrdenes;
        }
    }
}
