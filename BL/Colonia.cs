using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities()){
                    var colonias = context.ColoniaGetByIdMunicipio(IdMunicipio).ToList();
                    if(colonias.Count() != 0){
                        result.Objects = new List<object>();
                        foreach(var coloniaGet in colonias) {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = coloniaGet.IdColonia;
                            colonia.Nombre = coloniaGet.Nombre;
                            result.Objects.Add(colonia);
                        }
                        result.Correct = true;
                    } else {
                        result.Correct = false; result.ErrorMessage = "No se encontraron registros/datos";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
