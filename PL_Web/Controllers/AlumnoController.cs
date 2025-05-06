using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PL_Web.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        [HttpGet]
        public ActionResult GetAll()
        {
            AlumnoReference.AlumnoClient alumnoClient = new AlumnoReference.AlumnoClient();
            var result = alumnoClient.GetAll();
            ML.Alumno alumno = new ML.Alumno();
            if (result.Correct)
            {
                alumno.Alumnos = result.Objects.ToList();
            }
            else
            {
                alumno.Alumnos = new List<object>();
            }
            return View(alumno);
        }

        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();

            if (IdAlumno == null) {
                alumno.Materia = new ML.Materia
                {
                    //Materias = resultMateria.ToString()
                };
            }
            else
            {

            }
            return View(alumno);
        }
    }
}