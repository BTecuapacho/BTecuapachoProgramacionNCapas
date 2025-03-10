using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var municipios = context.MunicipioGetByIdEstado(IdEstado).ToList();
                    if (municipios.Count() != 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var municipioGet in municipios)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = municipioGet.IdMunicipio;
                            municipio.Nombre = municipioGet.Nombre;
                            result.Objects.Add(municipio);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No se encontraron registros/datos";
                    }
                }
            }
            catch (Exception ex) {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
