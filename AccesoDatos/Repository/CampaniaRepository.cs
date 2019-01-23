using DataModule;
using DataModule.Entities;
using DataModule.EntitiesResult;
using Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;
using System.Data.SqlClient;

namespace AccesoDatos.Repository
{
    public class CampaniaRepository: PublinterRepository, ICampaniaRepository
    {
        public CampaniaRepository(AppUser context) : base(context) { }


        public List<Get_all_campania> GetAll()
        {
            List<Get_all_campania> lista = new List<Get_all_campania>();
            using (var context = new PublinterContext())
            {
                lista = context.Database.SqlQuery<Get_all_campania>("GET_ALL_CAMPANIA").ToList();
            }
            return lista;
        }
        public Campania Get(int id)
        {
            Campania campaniaBd;
            using (var context = new PublinterContext())
            {
                campaniaBd = context.Campania
                            .Include("Cliente")
                            .Include("Anunciante")
                            .Include("Materiales")
                            .FirstOrDefault(x => x.CampaniaId.Equals(id));
            }
            return campaniaBd;
        }


        public int Add(Campania model)
        {
            using (var context = new PublinterContext())
            {
                context.Campania.Add(model);
                context.SaveChanges();
            }
                return model.CampaniaId;
        }

        public bool Update(Campania model)
        {
            try
            {
                using (var context = new PublinterContext())
                {
                    var campaniaBd = context.Campania
                        .Include(x => x.Materiales)
                        .FirstOrDefault(x => x.CampaniaId.Equals(model.CampaniaId));
                    if (campaniaBd == null) throw new Exception(string.Format("No se encontro la campaña."));

                    campaniaBd.Nombre = model.Nombre;
                    campaniaBd.ClienteId = model.ClienteId;
                    //campaniaBd.MedioId = model.MedioId;


                    // Materiales eliminados
                    var MaterialesDelete = model.Materiales.Where(x => x.Delete.Equals(true)).ToList();
                    
                    if (MaterialesDelete.Count > 0)
                    {
                        foreach(var i in MaterialesDelete)
                        {
                            var a = context.Material.FirstOrDefault(x => x.MaterialId.Equals(i.MaterialId));
                            if (a != null)
                                context.Entry(a).State = System.Data.Entity.EntityState.Deleted;
                            //MaterialesDelete.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Deleted);
                            context.SaveChanges();
                        }
                        
                        
                        
                    }

                    //materiales modificados
                    var MaterialesUpdate = model.Materiales.Where(x => x.MaterialId > 0 && x.Delete.Equals(false)).ToList();
                    if (MaterialesUpdate.Count > 0)
                    {
                        UpdateMateriales(campaniaBd, MaterialesUpdate);
                        campaniaBd.Materiales.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Modified);
                        context.SaveChanges();
                    }

                    // materiales nuevos
                    var MaterialesNuevos = model.Materiales.Where(x => x.Delete.Equals(false) && x.MaterialId == 0 && x.Titulo != "" && x.DuracionSegundos > 0).ToList();
                    if (MaterialesNuevos.Count > 0)
                    {
                        SaveNewMaterial(campaniaBd, MaterialesNuevos);
                        MaterialesNuevos.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Added);
                        context.SaveChanges();
                    }

                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }

        public void SaveNewMaterial(Campania campaniaBd, List<Material> nuevos)
        {
            foreach (var c in nuevos)
            {
                campaniaBd.Materiales.Add(c);
            }

        }

        public void UpdateMateriales(Campania campaniaBd, List<Material> Nuevos)
        {
            foreach (var matActual in Nuevos)
            {
                var existingInBd = campaniaBd.Materiales
                                    .Where(m => m.MaterialId == matActual.MaterialId)
                                    .SingleOrDefault();

                if (existingInBd != null)
                {
                    existingInBd.TipoMaterialId = matActual.TipoMaterialId;
                    existingInBd.Descripcion = matActual.Descripcion;
                    existingInBd.DuracionSegundos = matActual.DuracionSegundos;
                    existingInBd.Titulo = matActual.Titulo;
                }
            }
        }

        public List<Get_Material_Data> GetMaterialesByCampania(int campaniaId)
        {
            List<Get_Material_Data> materiales;
            using (var context = new PublinterContext())
            {
                var _cId = new SqlParameter("@CampaniaId", campaniaId);
                materiales = context.Database.SqlQuery<Get_Material_Data>("GET_MATERIALES_BY_CAMPANIA @CampaniaId", _cId).ToList();
            }
            return materiales;
        }
    }
}
