using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        [HttpPost]
        [Route("GetAll")]
        public IHttpActionResult GetAll([FromBody] ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            if (usuario == null)
            {
                result.Correct = false; result.ErrorMessage = "El cuerpo de la solicitud no puede estar vacío (Campos requeridos aunque sean vacios Nombre, Apellido Paterno, Apellido Materno, IdRol no puede ser nulo pero si 0).";
                return Content(HttpStatusCode.BadRequest, result);
            }

            result = BL.Usuarios.GetAllEF(usuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, result);
            }
        }

        [HttpGet]
        [Route("GetById/{IdUsuario}")]
        public IHttpActionResult GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            if (IdUsuario <= 0)
            {
                result.Correct = false; result.ErrorMessage = "El ID de usuario debe ser mayor a 0.";
                return Content(HttpStatusCode.BadRequest, result);
            }

            result = BL.Usuarios.GetByIDEF(IdUsuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, result);
            }
        }

        [HttpDelete]
        [Route("Delete/{IdUsuario}")]
        public IHttpActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            if (IdUsuario <= 0)
            {
                result.Correct = false; result.ErrorMessage = "El ID de usuario debe ser mayor a 0.";
                return Content(HttpStatusCode.BadRequest, result);
            }

            result = BL.Usuarios.DeleteEF(IdUsuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, result);
            }
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            if (usuario == null)
            {
                result.Correct = false; result.ErrorMessage = "El cuerpo de la solicitud no puede estar vacío.";
                return Content(HttpStatusCode.BadRequest, result);
            }
            try
            {
                usuario.Imagen = !string.IsNullOrEmpty(usuario.ImagenBase64) ? Convert.FromBase64String(usuario.ImagenBase64) : null;
                result = BL.Usuarios.AddEF(usuario);
                if (result.Correct)
                {
                    return Content(HttpStatusCode.OK, result);
                }
                else
                {
                    return Content(HttpStatusCode.InternalServerError, result);
                }
            }
            catch (FormatException)
            {
                result.Correct = false; result.ErrorMessage = "Error al convertir la imagen en Base64.";
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpPut]
        [Route("Update/{IdUsuario}")]
        public IHttpActionResult Update(int IdUsuario, [FromBody] ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            if (IdUsuario <= 0)
            {
                result.Correct = false; result.ErrorMessage = "El ID de usuario debe ser mayor a 0.";
                return Content(HttpStatusCode.BadRequest, result);
            }
            if (usuario == null)
            {
                result.Correct = false; result.ErrorMessage = "El cuerpo de la solicitud no puede estar vacío.";
                return Content(HttpStatusCode.BadRequest, result);
            }
            try
            {
                usuario.IdUsuario = IdUsuario;
                usuario.Imagen = !string.IsNullOrEmpty(usuario.ImagenBase64) ? Convert.FromBase64String(usuario.ImagenBase64) : null;
                result = BL.Usuarios.UpdateEF(usuario);
                if (result.Correct)
                {
                    return Content(HttpStatusCode.OK, result);
                }
                else
                {
                    return Content(HttpStatusCode.InternalServerError, result);
                }
            }
            catch (FormatException)
            {
                result.Correct = false; result.ErrorMessage = "Error al convertir la imagen en Base64.";
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}
