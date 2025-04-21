using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    public class CalculadoraController : ApiController
    {
        [HttpPost]
        [Route("api/Suma")]
        public IHttpActionResult Suma([FromBody] dynamic data)
        {
            double a = data.a;
            double b = data.b;
            double suma = a + b;
            return Content(HttpStatusCode.OK, suma);
        }

        [HttpPost]
        [Route("api/Resta")]
        public IHttpActionResult Resta([FromBody] dynamic data)
        {
            double a = data.a;
            double b = data.b;
            double resultado = a - b;
            return Content(HttpStatusCode.OK, resultado);
        }

        [HttpPost]
        [Route("api/Multipliacion")]
        public IHttpActionResult multipliacion([FromBody] dynamic data)
        {
            double a = data.a;
            double b = data.b;
            double multiplicasion = a + b;
            return Content(HttpStatusCode.OK, multiplicasion);
        }

        [HttpPost]
        [Route("api/Division")]
        public IHttpActionResult Division([FromBody] dynamic data)
        {
            double a = data.a;
            double b = data.b;
            double resultado = b == 0 ? 0 : a / b;
            return Content(HttpStatusCode.OK, resultado);
        }

        [HttpPost]
        public IHttpActionResult Calculadora([FromBody] dynamic data)
        {
            double a = data.a;
            double b = data.b;
            string operacion = data.operacion;

            double resultado;
            switch (operacion)
            {
                case "+":
                    resultado = a + b;
                    break;
                case "-":
                    resultado = a - b;
                    break;
                case "*":
                    resultado = a * b;
                    break;
                case "/":
                    resultado = b == 0 ? 0 : a / b;
                    break;
                default:
                    resultado = 0;
                    break;
            }
            return Content(HttpStatusCode.OK, resultado);
        }
    }
}
