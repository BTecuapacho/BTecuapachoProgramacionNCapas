using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Usuario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Usuario.svc or Usuario.svc.cs at the Solution Explorer and start debugging.
    public class Usuario : IUsuario
    {
        public SL_WCF.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuarios.GetAllEF(usuario);
            return new SL_WCF.Result { 
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects
            };
        }

        public SL_WCF.Result GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuarios.GetByIDEF(IdUsuario);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects
            };
        }   

        public SL_WCF.Result Add(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuarios.AddEF(usuario);
            return new SL_WCF.Result {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects
            };
        }

        public SL_WCF.Result Update(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuarios.UpdateEF(usuario);
            return new SL_WCF.Result {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects
            };
        }

        public SL_WCF.Result Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuarios.DeleteEF(IdUsuario);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects
            };
        }
    }
}
