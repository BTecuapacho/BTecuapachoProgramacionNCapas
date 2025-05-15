using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sucursal
    {
        public static ML.Result GetAllDDL()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var Sucursales = (from Sucursal in context.Sucursals
                                      select new
                                      {
                                          IdSucursal = Sucursal.IdSucursal,
                                          Nombre = Sucursal.Nombre
                                      }).ToList();
                    if (Sucursales.Count != 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var dbSucursal in Sucursales)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal();
                            sucursal.IdSucursal = Convert.ToInt32(dbSucursal.IdSucursal);
                            sucursal.Nombre = dbSucursal.Nombre;
                            result.Objects.Add(sucursal);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No hay Datos/Registros.";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var Sucursales = (from Sucursal in context.Sucursals
                                      select new
                                      {
                                          IdSucursal = Sucursal.IdSucursal,
                                          Nombre = Sucursal.Nombre,
                                          Latitud = Sucursal.Latitud,
                                          Longitud = Sucursal.Longitud
                                      }).ToList();
                    if (Sucursales.Count != 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var dbSucursal in Sucursales)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal();
                            sucursal.IdSucursal = Convert.ToInt32(dbSucursal.IdSucursal);
                            sucursal.Nombre = dbSucursal.Nombre;
                            sucursal.Latitud = dbSucursal.Latitud;
                            sucursal.Longitud = dbSucursal.Longitud;
                            result.Objects.Add(sucursal);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No hay Datos/Registros.";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int IdSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var dbSucursal = (from sucursal in context.Sucursals
                                      where sucursal.IdSucursal == IdSucursal
                                      select new
                                      {
                                          IdSucursal = sucursal.IdSucursal,
                                          Nombre = sucursal.Nombre,
                                          Latitud = sucursal.Latitud,
                                          Longitud = sucursal.Longitud
                                      }).Single();
                    if (dbSucursal != null)
                    {
                        ML.Sucursal sucursal = new ML.Sucursal();
                        sucursal.IdSucursal = Convert.ToInt32(dbSucursal.IdSucursal);
                        sucursal.Nombre = dbSucursal.Nombre;
                        sucursal.Latitud = dbSucursal.Latitud;
                        sucursal.Longitud = dbSucursal.Longitud;
                        result.Object = sucursal;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No de encontro la sucursal";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.Sucursal sucursalIn)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    DL_EF.Sucursal sucursalInsert = new DL_EF.Sucursal {
                        Nombre = sucursalIn.Nombre,
                        Latitud = sucursalIn.Latitud,
                        Longitud = sucursalIn.Longitud
                    };
                    context.Sucursals.Add(sucursalInsert);
                    int filasAfectadas = context.SaveChanges();
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al insertar la sucursal";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Sucursal sucursalIn)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var dbSucursal = (from sucursal in context.Sucursals
                                      where sucursal.IdSucursal == sucursalIn.IdSucursal
                                      select sucursal).Single();
                    if (dbSucursal != null)
                    {
                        dbSucursal.Nombre = sucursalIn.Nombre;
                        dbSucursal.Longitud = sucursalIn.Longitud;
                        dbSucursal.Longitud = sucursalIn.Longitud;
                        int filasAfectadas = context.SaveChanges();
                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = true; result.ErrorMessage = "Error al actualizar la sucursal.";
                        }
                    }
                    else
                    {
                        result.Correct = true; result.ErrorMessage = "Sucursal no encontrada.";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        } 

        public static ML.Result Delete(int IdSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var dbSucursal = (from sucursal in context.Sucursals
                                      where sucursal.IdSucursal == IdSucursal
                                      select sucursal).Single();
                    if(dbSucursal != null)
                    {
                        context.Sucursals.Remove(dbSucursal);
                        int filasAfectadas = context.SaveChanges();
                        if(filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false; result.ErrorMessage = "Error al eliminar la sucursal.";
                        }
                    }else
                    {
                        result.Correct = false; result.ErrorMessage = "No se encontro la sucursal.";
                    }
                }
            }catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
