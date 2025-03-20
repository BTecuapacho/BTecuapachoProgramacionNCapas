using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Program
    {
        static void Main(string[] args)
        {
            Repetir();
        }

        public static void Menu()
        {
            Console.WriteLine("CRUD Usuarios");
            Console.WriteLine("Seleccione una opcion:");
            Console.WriteLine("1) Mostrar Usuarios");
            Console.WriteLine("2) Crear usuario");
            Console.WriteLine("3) Actualizar Usuario");
            Console.WriteLine("4) Carga masiva TXT");
            Console.WriteLine("5) Borrar usuario");
            Swich();
        }

        public static void Swich()
        {

            int opcion = Convert.ToInt32(Console.ReadLine());
            switch (opcion)
            {
                case 1:
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Console.WriteLine("Mostrando Todo.");
                    //Usuario.GetAll();
                    //Usuario.GetByID();
                    break;
                case 2:
                    Console.WriteLine("Registro de Usuarios");
                    //Usuario.Add();
                    break;
                case 3:
                    Console.WriteLine("Actualización de Usuario");
                    //Usuario.Update();
                    break;
                case 4:
                    Console.WriteLine("Carga masiva de usuarios");
                    Usuario.AddMasivo();
                    break;
                case 5:
                    Console.WriteLine("Eliminar Usuario");
                    //Usuario.Delete();
                    break;
                default:
                    Console.WriteLine("Seleción invalida.");
                    break;
            }
        }

        public static void Repetir()
        {
            int respuesta;
            do
            {
                Menu();
                Console.WriteLine("Desea repetir el proceso si = 1, no = 0:");
                respuesta = Convert.ToInt32(Console.ReadLine());
            } while (respuesta == 1);
        }
    }
}
