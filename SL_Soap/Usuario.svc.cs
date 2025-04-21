using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_Soap
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Usuario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Usuario.svc or Usuario.svc.cs at the Solution Explorer and start debugging.
    public class Usuario : IUsuario
    {
        public SL_Soap.Result GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuarios.GetByIDEF(IdUsuario);
            return new SL_Soap.Result {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Exception = result.Exception,
                Object = result.Object,
                Objects = result.Objects
            };
        }
    }
}
