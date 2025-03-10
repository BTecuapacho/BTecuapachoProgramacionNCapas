using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Usuario
    {
        public static void Adds()
        {
            string respuesta;
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el nombre del Usuario");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el Apellido paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Ingrese el Apellido materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Ingresa el correo del usuario");
            usuario.Email = Console.ReadLine();
            Console.WriteLine(respuesta = BL.Usuarios.Adds(usuario) ?
                "El Usuario se inserto de manera correcta!." :
                "Error al insertar el uasuario!.");
        }

        public static void Updates() { 
            string respuesta;
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el Id del Usuario que actualizar.");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese el nombre del Usuario");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el Apellido paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Ingrese el Apellido materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Ingresa el correo del usuarios");
            usuario.Email = Console.ReadLine();
            Console.WriteLine(respuesta = BL.Usuarios.Updates(usuario) ? 
                "El Usuario se actualizo de manera correcta!." : 
                "Error al actualizar el uasuario!.");
        }

        public static void Deletes()
        {
            string respuesta;
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el Id del Usuario que desea eliminar");
            int idUsuario = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(respuesta = BL.Usuarios.Deletes(idUsuario)? 
                "El Usuario se elimino de manera correcta!." : 
                "Error al eliminar el uasuario!.");
        }

        public static void Exists()
        {
            string respuesta;
            Console.WriteLine("Ingrese el Id del usuario que desea verificar existencia");
            int idUsuario = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(respuesta = BL.Usuarios.Exists(idUsuario) ? 
                "El Usuario si exite!." : 
                "Error el uasuario no existe!.");

        }

        public static void ShowOne()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el Id del usuario que desea mostrar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());
            BL.Usuarios.ShowOne(usuario);

        }
        
        public static void ShowAll()
        {
            Console.WriteLine("Lista de usuarios");
            BL.Usuarios.ShowAll();
        }

        //Usando Result
        public static void AddQuery()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el nombre del Usuario");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el Apellido paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Ingrese el Apellido materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Ingresa el correo del usuario");
            usuario.Email = Console.ReadLine();
            ML.Result result = BL.Usuarios.Add(usuario);
            if (result.Correct)
            {
                Console.WriteLine("El Usuario se inserto de manera correcta");
            }
            else
            {
                Console.WriteLine("Hubo un error: " + result.ErrorMessage);
            }
        }

        public static void UpdateQuery()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el Id del Usuario que actualizar.");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());
            ML.Result resultExist = BL.Usuarios.ExistQuery(usuario.IdUsuario);
            if (resultExist.Correct)
            {

                ML.Result resultMostrarUsuario = BL.Usuarios.GetByIDQuery(usuario.IdUsuario);
                if (resultMostrarUsuario.Correct)
                {
                    ML.Usuario usuarioMostrar = (ML.Usuario)resultMostrarUsuario.Object;
                    Console.WriteLine("IdUsuario: " + usuarioMostrar.IdUsuario +
                                      ", Nombre: " + usuarioMostrar.Nombre + " " + usuarioMostrar.ApellidoPaterno + " " + usuarioMostrar.ApellidoMaterno +
                                      ", Correo: " + usuarioMostrar.Email);
                }
                else
                {
                    Console.WriteLine("Hubo un error: " + resultMostrarUsuario.ErrorMessage);
                }
                Console.WriteLine("---------------------------------------------------------------------------");
                Console.WriteLine("Ingrese el nuevo nombre del Usuario");
                usuario.Nombre = Console.ReadLine();
                Console.WriteLine("Ingrese el nuevo Apellido paterno del usuario");
                usuario.ApellidoPaterno = Console.ReadLine();
                Console.WriteLine("Ingrese el nuevo Apellido materno del usuario");
                usuario.ApellidoMaterno = Console.ReadLine();
                Console.WriteLine("Ingresa el nuevo correo del usuarios");
                usuario.Email = Console.ReadLine();
                ML.Result result = BL.Usuarios.UpdateQuery(usuario);
                if (result.Correct)
                {
                    Console.WriteLine("El Usuario seactualizo de manera correcta");
                }
                else
                {
                    Console.WriteLine("Hubo un error: " + result.ErrorMessage);
                }
            } else
            {
                Console.WriteLine("Hubo un error: " + resultExist.ErrorMessage);
            }
        }

        public static void DeleteQuery()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el Id del Usuario que eliminar.");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());
            ML.Result resultExist = BL.Usuarios.ExistQuery(usuario.IdUsuario);
            if (resultExist.Correct)
            {

                ML.Result resultMostrarUsuario = BL.Usuarios.GetByIDQuery(usuario.IdUsuario);
                if (resultMostrarUsuario.Correct)
                {
                    ML.Usuario usuarioMostrar = (ML.Usuario)resultMostrarUsuario.Object;
                    Console.WriteLine("IdUsuario: " + usuarioMostrar.IdUsuario +
                                      ", Nombre: " + usuarioMostrar.Nombre + " " + usuarioMostrar.ApellidoPaterno + " " + usuarioMostrar.ApellidoMaterno +
                                      ", Correo: " + usuarioMostrar.Email);
                }
                else
                {
                    Console.WriteLine("Hubo un error: " + resultMostrarUsuario.ErrorMessage);
                }
                Console.WriteLine("---------------------------------------------------------------------------");
                ML.Result result = BL.Usuarios.DeleteQuery(usuario.IdUsuario);
                if (result.Correct)
                {
                    Console.WriteLine("El Usuario se elimino de manera correcta");
                }
                else
                {
                    Console.WriteLine("Hubo un error: " + result.ErrorMessage);
                }
            }
            else
            {

                Console.WriteLine("Hubo un error: " + resultExist.ErrorMessage);
            }
        }

        public static void GetAllQuery()
        {
            ML.Result result = BL.Usuarios.GetAllQuery();
            if (result.Correct)
            {
                foreach(ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine("IdUsuario: " + usuario.IdUsuario +
                                      ", Nombre: " + usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno +
                                      ", Correo: " + usuario.Email);
                }
            }
            else
            {
                Console.WriteLine("Hubo un error: " + result.ErrorMessage);
            }
        }

        public static void GetByIDQuery()
        {
            int idUsuario;
            Console.WriteLine("Ingrese el Id del usuario que desea mostrar");
            idUsuario = Convert.ToInt32(Console.ReadLine());
            ML.Result result = BL.Usuarios.GetByIDQuery(idUsuario);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                Console.WriteLine("IdUsuario: " + usuario.IdUsuario +
                                  ", Nombre: " + usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno +
                                  ", Correo: " + usuario.Email);
            }
            else
            {
                Console.WriteLine("Hubo un error: " + result.ErrorMessage);
            }
        }

        //Usando Result y Procedimientos Almacenados
        public static void Add()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el UserName");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("Ingrese el nombre del Usuario");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el Apellido paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Ingrese el Apellido materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Ingresa el correo del usuario");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingresa el password del usuario");
            usuario.Password = Console.ReadLine(); 
            Console.WriteLine("Ingrese una fecha de nacimiento del usuario en número (formato: dia/mes/año):");
            usuario.FechaNacimiento = Console.ReadLine();
            Console.WriteLine("Ingresa el Sexo del usuario Hombre = M, Mujer = F");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("Ingresa el telefono del usuario");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("Ingresa el celular del usuario");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("Ingresa el Estatus del usuario 1 = Habielitado 2 = Deshabilitado");
            int estatus = Convert.ToInt32(Console.ReadLine());
            usuario.Estatus = estatus == 1 ? true : false;
            Console.WriteLine("Ingresa el CURP del usuario");
            usuario.CURP = Console.ReadLine();
            //ML.Result result = BL.Usuarios.Add(usuario);
            //ML.Result result = BL.Usuarios.AddEF(usuario);
            ML.Result result = BL.Usuarios.AddLQ(usuario);
            Console.WriteLine(
                result.Correct ? "El Usuario se inserto de manera correcta" : 
                "Hubo un error: " + result.ErrorMessage);
        }

        public static void Update()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el Id del Usuario que actualizar.");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());
            //ML.Result resultExist = BL.Usuarios.Exist(usuario.IdUsuario);
            //ML.Result resultExist = BL.Usuarios.ExistEF(usuario.IdUsuario);
            ML.Result resultExist = BL.Usuarios.ExistLQ(usuario.IdUsuario);
            if (resultExist.Correct)
            {

                //ML.Result resultMostrarUsuario = BL.Usuarios.GetByID(usuario.IdUsuario);
                //ML.Result resultMostrarUsuario = BL.Usuarios.GetByIDEF(usuario.IdUsuario);
                ML.Result resultMostrarUsuario = BL.Usuarios.GetByIDLQ(usuario.IdUsuario);
                if (resultMostrarUsuario.Correct)
                {
                    ML.Usuario usuarioMostrar = (ML.Usuario)resultMostrarUsuario.Object;
                    Console.WriteLine("IdUsuario: " + usuarioMostrar.IdUsuario +
                                      ", Nombre: " + usuarioMostrar.Nombre + " " + usuarioMostrar.ApellidoPaterno + " " + usuarioMostrar.ApellidoMaterno +
                                      ", Correo: " + usuarioMostrar.Email);
                }
                else
                {
                    Console.WriteLine("Hubo un error: " + resultMostrarUsuario.ErrorMessage);
                }
                Console.WriteLine("Ingrese el nuevo UserName");
                usuario.UserName = Console.ReadLine();
                Console.WriteLine("Ingrese el nuevo nombre del Usuario");
                usuario.Nombre = Console.ReadLine();
                Console.WriteLine("Ingrese el nuevo Apellido paterno del usuario");
                usuario.ApellidoPaterno = Console.ReadLine();
                Console.WriteLine("Ingrese el nuevo Apellido materno del usuario");
                usuario.ApellidoMaterno = Console.ReadLine();
                Console.WriteLine("Ingresa el nuevo correo del usuario");
                usuario.Email = Console.ReadLine();
                Console.WriteLine("Ingresa el nuevo password del usuario");
                usuario.Password = Console.ReadLine();
                Console.WriteLine("Ingrese una nuevo fecha de nacimiento del usuario en número (formato: dia/mes/año):");
                usuario.FechaNacimiento = Console.ReadLine();
                Console.WriteLine("Ingresa el nuevo Sexo del usuario Hombre = M, Mujer = F");
                usuario.Sexo = Console.ReadLine();
                Console.WriteLine("Ingresa el nuevo telefono del usuario");
                usuario.Telefono = Console.ReadLine();
                Console.WriteLine("Ingresa el nuevo celular del usuario");
                usuario.Celular = Console.ReadLine();
                Console.WriteLine("Ingresa el nuevo Estatus del usuario 1 = Habielitado 2 = Deshabilitado");
                int estatus = Convert.ToInt32(Console.ReadLine());
                usuario.Estatus = estatus == 1 ? true : false;
                Console.WriteLine("Ingresa el nuevo CURP del usuario");
                usuario.CURP = Console.ReadLine();
                //ML.Result result = BL.Usuarios.Update(usuario);
                //ML.Result result = BL.Usuarios.UpdateEF(usuario);
                ML.Result result = BL.Usuarios.UpdateLQ(usuario);
                Console.WriteLine(
                    result.Correct ? "El Usuario se actualizo de manera correcta" :
                    "Hubo un error: " + result.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Hubo un error: " + resultExist.ErrorMessage);
            }
        }

        public static void Delete()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el Id del Usuario que eliminar.");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());
            //ML.Result resultExist = BL.Usuarios.Exist(usuario.IdUsuario);
            //ML.Result resultExist = BL.Usuarios.ExistEF(usuario.IdUsuario);
            ML.Result resultExist = BL.Usuarios.ExistLQ(usuario.IdUsuario);
            if (resultExist.Correct)
            {

                //ML.Result resultMostrarUsuario = BL.Usuarios.GetByID(usuario.IdUsuario);
                //ML.Result resultMostrarUsuario = BL.Usuarios.GetByIDEF(usuario.IdUsuario);
                ML.Result resultMostrarUsuario = BL.Usuarios.GetByIDLQ(usuario.IdUsuario);
                if (resultMostrarUsuario.Correct)
                {
                    ML.Usuario usuarioMostrar = (ML.Usuario)resultMostrarUsuario.Object;
                    Console.WriteLine("IdUsuario: " + usuarioMostrar.IdUsuario +
                                      ", Nombre: " + usuarioMostrar.Nombre + " " + usuarioMostrar.ApellidoPaterno + " " + usuarioMostrar.ApellidoMaterno +
                                      ", Correo: " + usuarioMostrar.Email);
                }
                else
                {
                    Console.WriteLine("Hubo un error: " + resultMostrarUsuario.ErrorMessage);
                }
                Console.WriteLine("---------------------------------------------------------------------------");
                //ML.Result result = BL.Usuarios.Delete(usuario.IdUsuario);
                //ML.Result result = BL.Usuarios.DeleteEF(usuario.IdUsuario);
                ML.Result result = BL.Usuarios.DeleteLQ(usuario.IdUsuario);
                if (result.Correct)
                {
                    Console.WriteLine("El Usuario se elimino de manera correcta");
                }
                else
                {
                    Console.WriteLine("Hubo un error: " + result.ErrorMessage);
                }
            }
            else
            {

                Console.WriteLine("Hubo un error: " + resultExist.ErrorMessage);
            }
        }

        public static void GetAll()
        {
            //ML.Result result = BL.Usuarios.GetAll();
            //ML.Result result = BL.Usuarios.GetAllEF();
            ML.Result result = BL.Usuarios.GetAllLQ();
            if (result.Correct)
            {
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine("IdUsuario: " + usuario.IdUsuario +
                                      ", Nombre: " + usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno +
                                      ", Correo: " + usuario.Email + ", Fecha de Nacimiento: " + usuario.FechaNacimiento);
                }
            }
            else
            {
                Console.WriteLine("Hubo un error: " + result.ErrorMessage);
            }
        }
        
        public static void GetByID()
        {
            int idUsuario;
            Console.WriteLine("Ingrese el Id del usuario que desea mostrar");
            idUsuario = Convert.ToInt32(Console.ReadLine());
            //ML.Result result = BL.Usuarios.GetByID(idUsuario);
            //ML.Result result = BL.Usuarios.GetByIDEF(idUsuario);
            ML.Result result = BL.Usuarios.GetByIDLQ(idUsuario);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                Console.WriteLine("IdUsuario: " + usuario.IdUsuario +
                                  ", Nombre: " + usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno +
                                  ", Correo: " + usuario.Email + ", Fecha de Nacimiento: " + usuario.FechaNacimiento);
            }
            else
            {
                Console.WriteLine("Hubo un error: " + result.ErrorMessage);
            }
        }
    }
}
