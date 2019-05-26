using DataModule;
using DataModule.Entities;
using Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repository
{
    public class TipoMaterialRepository: PublinterRepository, ITipoMaterialRepository
    {

        public TipoMaterialRepository(AppUser context) : base(context)
        {

        }
        public bool Create(TipoMaterial model)
        {
            try
            {
                using (var context = new PublinterContext())
                {
                    TipoMaterial existe = context.TipoMaterial.FirstOrDefault(x => x.Descripcion.Equals(model.Descripcion));
                    if (existe != null) throw new System.ArgumentException("Ya existe un tipo con esa descripcion");
                    context.TipoMaterial.Add(model);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public List<TipoMaterial> GetAll()
        {
            List<TipoMaterial> lista;
            using (var context = new PublinterContext())
            {
                lista = context.TipoMaterial.Where(x => x.Deshabilitado == false).ToList();
            }
            return lista;
        }
        public bool DeshabilitarTipoMaterial(int id)
        {
            try
            {
                using (var context = new PublinterContext())
                {
                    var buscado = context.TipoMaterial.FirstOrDefault(x => x.TipoMaterialId.Equals(id));
                    if(buscado == null) throw new System.ArgumentException("No se encontro el tipomaterial");
                    buscado.Deshabilitado = true;
                    context.Entry(buscado).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                    return true;
            }
            catch(Exception e)
            {

                return false;
            }
        }
    }
}
