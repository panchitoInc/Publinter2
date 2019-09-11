using DataModule;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity;

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

                    if (nueva.OrdenDeCompraId.HasValue && nueva.OrdenDeCompraId.Value != 0)
                    {
                        OrdenDeCompra compra = context.OrdenDeCompra.FirstOrDefault(x => x.OrdenDeCompraId.Equals(nueva.OrdenDeCompraId.Value));
                        if(compra.Saldo > nueva.TotalSegundos)
                        {
                            compra.Saldo -= nueva.TotalSegundos;
                        }
                        else
                        {
                            nueva.TotalSegundos -= compra.Saldo;
                            compra.Saldo = 0;
                        }

                        context.Entry(compra).State = System.Data.Entity.EntityState.Modified;
                    }

                    if (nueva.AnulaA.HasValue)
                    {
                        Orden anulada = context.Orden.FirstOrDefault(x => x.OrdenId.Equals(nueva.AnulaA.Value));
                        anulada.Anulada = true;
                        anulada.AnuladaPor = nueva.OrdenId;
                        context.Entry(anulada).State = System.Data.Entity.EntityState.Modified;
                    }

                    context.SaveChanges();

                    return nueva.OrdenId;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public IEnumerable<Get_orden_index> GetIndex(int start, int length, int sortColumn, string sortDirection, string search)
        {
            IEnumerable<Get_orden_index> ListaOrdenes = new List<Get_orden_index>();

            using (var context = new PublinterContext())
            {
                var _usuid = new SqlParameter("@USUARIOID", this.Context.Id);
                var _desde = new SqlParameter("@DESDE", start);
                var _cantidad = new SqlParameter("@CANTIDAD", length);
                var _sortColumn = new SqlParameter("@SORTCOLUMN", sortColumn);
                var _sortDirection = new SqlParameter("@SORTDIRECTION", sortDirection);
                var _search = new SqlParameter("@SEARCH", search);

                ListaOrdenes = context.Database.SqlQuery<Get_orden_index>("INDEX_ORDEN_DATA_PAGE @USUARIOID, @DESDE, @CANTIDAD, @SORTCOLUMN, @SORTDIRECTION, @SEARCH", _usuid, _desde, _cantidad, _sortColumn, _sortDirection, _search).ToList();
            }

            return ListaOrdenes;
        }

        public Orden Get(int ordenId)
        {
            Orden ordenBusacada;
            using (var context = new PublinterContext())
            {
                ordenBusacada = context.Orden
                    .Include(x => x.Campania)
                    //.Include(x => x.Campania.Anunciante)
                    .Include(x => x.LineasOrden)
                    .Include(x => x.LineasOrden.Select(lOrden => lOrden.LineasInternasOrden))
                    .Include(x => x.LineasOrden.Select(lOrden => lOrden.LineasInternasOrden.Select(m => m.Mes)))
                    .Include(x => x.LineasOrden.Select(lOrden => lOrden.LineasInternasOrden.Select(m => m.Mes).Select(dias => dias.Dias)))
                    .Include(x =>x.Usuario)
                    //.Include(x => x.Medio)
                    .FirstOrDefault(x => x.OrdenId.Equals(ordenId));
            }
            return ordenBusacada;
        }

        public List<Get_Orden_Select> GetOrdenesSelect(int campaniaId, int medioId)
        {
            List<Get_Orden_Select> ListaOrdenes = new List<Get_Orden_Select>();

            using (var context = new PublinterContext())
            {
                var _campid = new SqlParameter("@CAMPANIAID", campaniaId);
                var _medioid = new SqlParameter("@MEDIOID", medioId);

                ListaOrdenes = context.Database.SqlQuery<Get_Orden_Select>("GET_ORDENES_SELECT @CAMPANIAID, @MEDIOID", _campid, _medioid).ToList();
            }

            return ListaOrdenes;
        }
    }
}
