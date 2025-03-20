using DL;
using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuarios
    {
        //Sin result
        public static bool Adds(ML.Usuario usuario)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "INSERT INTO Usuario VALUES (@Nombre, @ApellidoPaterno , @ApellidoMaterno, @Email)";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    resultado = filasAfectadas != 0 ? true : false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                resultado = false;
            }
            return resultado;
        }

        public static bool Updates(ML.Usuario usuario)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "UPDATE Usuario SET " +
                                    "Nombre = @nombre, ApellidoPaterno = @apellidoPaterno, " +
                                    "ApellidoMaterno = @apellidoMaterno, Email = @email" +
                                    "WHERE IdUsuario = @idUsuario";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("@idUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@apellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@apellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    resultado = filasAfectadas != 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                resultado = false;
            }
            return resultado;
        }

        public static bool Deletes(int idUsuario)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "DELETE FROM Usuario WHERE IdUsuario = @idUsuario";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    resultado = filasAfectadas != 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                resultado = false;
            }
            return resultado;
        }

        public static bool Exists(int idUsuario)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "SELECT COUNT(*) FROM Usuario WHERE IdUsuario = @idUsuario";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    context.Open();
                    int contador = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.Clear();
                    resultado = contador != 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                resultado = false;
            }
            return resultado;
        }

        public static void ShowOne(ML.Usuario usuario)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "SELECT IdUsuario, Nombre, ApellidoPaterno , ApellidoMaterno, Email " +
                                    "FROM Usuario WHERE IdUsuario = @idUsuario";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("@idUsuario", usuario.IdUsuario);
                    context.Open();
                    SqlDataReader lector = cmd.ExecuteReader();
                    if (lector.Read())
                    {
                        usuario.Nombre = Convert.ToString(lector["Nombre"]);
                        usuario.ApellidoPaterno = Convert.ToString(lector["ApellidoPaterno"]);
                        usuario.ApellidoMaterno = Convert.ToString(lector["ApellidoMaterno"]);
                        usuario.Email = Convert.ToString(lector["Email"]);
                        Console.WriteLine("IdUsuario: " + usuario.IdUsuario +
                                        " ,Nombre: " + usuario.Nombre + " " + usuario.ApellidoPaterno +
                                        " " + usuario.ApellidoMaterno +
                                        " ,Correo: " + usuario.Email);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void ShowAll()
        {
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    ML.Usuario usuario = new ML.Usuario();
                    string query = "SELECT IdUsuario, Nombre, ApellidoPaterno , ApellidoMaterno, Email FROM Usuario";
                    SqlCommand cmd = new SqlCommand(query, context);
                    context.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        usuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        usuario.Nombre = Convert.ToString(reader["Nombre"]);
                        usuario.ApellidoPaterno = Convert.ToString(reader["ApellidoPaterno"]);
                        usuario.ApellidoMaterno = Convert.ToString(reader["ApellidoMaterno"]);
                        usuario.Email = Convert.ToString(reader["Email"]);
                        Console.WriteLine("IdUsuario: " + usuario.IdUsuario +
                                          ", Nombre: " + usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno +
                                          ", Correo: " + usuario.Email);
                        usuario.IdUsuario = 0;
                        usuario.Nombre = "";
                        usuario.ApellidoPaterno = "";
                        usuario.ApellidoMaterno = "";
                        usuario.Email = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        //Usando Result
        public static ML.Result AddQuery(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "INSERT INTO Usuario VALUES (@Nombre, @ApellidoPaterno , @ApellidoMaterno, @email)";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    if (filasAfectadas != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al registrar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateQuery(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "UPDATE Usuario SET " +
                                    "Nombre = @nombre, ApellidoPaterno = @apellidoPaterno, " +
                                    "ApellidoMaterno = @apellidoMaterno, Email = @email" +
                                    "WHERE IdUsuario = @idUsuario";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("@idUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@apellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@apellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    if (filasAfectadas != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al actualizar al usuario.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.ErrorMessage = ex.Message; result.Exception = ex;
            }
            return result;
        }

        public static ML.Result DeleteQuery(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "DELETE FROM Usuario WHERE IdUsuario = @idUsuario";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    if (filasAfectadas != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al borrar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.ErrorMessage = ex.Message; result.Exception = ex;
            }
            return result;
        }

        public static ML.Result ExistQuery(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "SELECT COUNT(*) FROM Usuario WHERE IdUsuario = @idUsuario";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    context.Open();
                    int filasAfectadas = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.Clear();
                    if (filasAfectadas != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Usuario no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.ErrorMessage = ex.Message; result.Exception = ex;
            }
            return result;
        }

        public static ML.Result GetAllQuery()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "SELECT *FROM Usuario";
                    SqlCommand cmd = new SqlCommand(query, context);
                    context.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count != 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                            usuario.Nombre = row[1].ToString();
                            usuario.ApellidoPaterno = row[2].ToString();
                            usuario.ApellidoMaterno = row[3].ToString();
                            usuario.Email = row[4].ToString();
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay Datos/Registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetByIDQuery(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "SELECT *FROM Usuario WHERE IdUsuario = @idUsuario";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    context.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count != 0)
                    {
                        result.Correct = true;
                        DataRow row = dataTable.Rows[0];
                        result.Object = new ML.Usuario
                        {
                            IdUsuario = Convert.ToInt32(row[0].ToString()),
                            Nombre = row[1].ToString(),
                            ApellidoPaterno = row[2].ToString(),
                            ApellidoMaterno = row[3].ToString(),
                            Email = row[4].ToString()
                        };
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Usuario no Encontrado";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Exception = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        //Usando Procedimientos Almacenados
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "UsuarioAdd";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    if (filasAfectadas != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al registrar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "UsuarioUpdate";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    if (filasAfectadas != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al actualizar al usuario.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.ErrorMessage = ex.Message; result.Exception = ex;
            }
            return result;
        }

        public static ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "UsuarioDelete";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    if (filasAfectadas != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al borrar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.ErrorMessage = ex.Message; result.Exception = ex;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "UsuarioGetAll";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    context.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count != 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                            usuario.Nombre = row[1].ToString();
                            usuario.ApellidoPaterno = row[2].ToString();
                            usuario.ApellidoMaterno = row[3].ToString();
                            usuario.Email = row[4].ToString();
                            usuario.Password = row[5].ToString();
                            usuario.FechaNacimiento = Convert.ToString(row[6].ToString());
                            usuario.Sexo = row[7].ToString();
                            usuario.Telefono = row[8].ToString();
                            usuario.Celular = row[9].ToString();
                            usuario.Estatus = Convert.ToBoolean(row[10].ToString());
                            usuario.CURP = row[11].ToString();
                            usuario.UserName = row[12].ToString();

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No hay Datos/Registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetByID(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "UsuarioGetById";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    context.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count != 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        result.Object = new ML.Usuario
                        {
                            IdUsuario = Convert.ToInt32(row[0].ToString()),
                            Nombre = row[1].ToString(),
                            ApellidoPaterno = row[2].ToString(),
                            ApellidoMaterno = row[3].ToString(),
                            Email = row[4].ToString(),
                            Password = row[5].ToString(),
                            FechaNacimiento = Convert.ToString(row[6].ToString()),
                            Sexo = row[7].ToString(),
                            Telefono = row[8].ToString(),
                            Celular = row[9].ToString(),
                            Estatus = Convert.ToBoolean(row[10].ToString()),
                            CURP = row[11].ToString(),
                            UserName = row[12].ToString()
                        };
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Usuario no Encontrado";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Exception = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Exist(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Coneccion.GetConeccion()))
                {
                    string query = "UsuarioExist";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    context.Open();
                    int filasAfectadas = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Parameters.Clear();
                    if (filasAfectadas != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Usuario no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.ErrorMessage = ex.Message; result.Exception = ex;
            }
            return result;
        }

        //Usando Entity Framework
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    DateTime fecha = DateTime.ParseExact(usuario.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //DateTime fecha = DateTime.ParseExact(usuario.FechaNacimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    int filasAfectadas = context.UsuarioAdd(usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, fecha, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.CURP, usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia, usuario.Imagen);

                    if (filasAfectadas == 2)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al registrar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    DateTime fecha = DateTime.ParseExact(usuario.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //DateTime fecha = DateTime.ParseExact(usuario.FechaNacimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    int filasAfectadas = context.UsuarioUpdate(usuario.IdUsuario, usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, fecha, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.CURP, usuario.Rol.IdRol, usuario.Direccion.IdDireccion, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia, usuario.Imagen);
                    if (filasAfectadas == 2)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al registrar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result DeleteEF(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    int filasAfectadas = context.UsuarioDelete(idUsuario);

                    if (filasAfectadas == 2)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al eliminar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAllEF(ML.Usuario usuarioIn)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    //var usuarios = context.UsuarioGetAll(usuarioIn.Nombre, usuarioIn.ApellidoPaterno, usuarioIn.ApellidoMaterno, usuarioIn.Rol.IdRol).ToList();
                    var usuarios = context.UsuarioGetAllWithView(usuarioIn.Nombre, usuarioIn.ApellidoPaterno, usuarioIn.ApellidoMaterno, usuarioIn.Rol.IdRol).ToList();
                    if (usuarios.Count() != 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var usuarioGet in usuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.IdUsuario = Convert.ToInt32(usuarioGet.IdUsuario);
                            usuario.Nombre = usuarioGet.NombreUsuario;
                            usuario.ApellidoPaterno = usuarioGet.ApellidoPaterno;
                            usuario.ApellidoMaterno = usuarioGet.ApellidoMaterno;
                            usuario.Email = usuarioGet.Email;
                            usuario.Password = usuarioGet.Password;
                            usuario.FechaNacimiento = Convert.ToString(usuarioGet.FechaNacimiento);
                            usuario.Sexo = usuarioGet.Sexo;
                            usuario.Telefono = usuarioGet.Telefono;
                            usuario.Celular = usuarioGet.Celular;
                            usuario.Estatus = Convert.ToBoolean(usuarioGet.Estatus);
                            usuario.CURP = usuarioGet.CURP;
                            usuario.UserName = usuarioGet.UserName;
                            usuario.Imagen = usuarioGet.Imagen;
                            usuario.Rol.Nombre = usuarioGet.NombreRol != null? usuarioGet.NombreRol:"Sin rol asignado";
                            usuario.Direccion.Calle = usuarioGet.calle;
                            usuario.Direccion.NumeroInterior = usuarioGet.NumeroInterior;
                            usuario.Direccion.NumeroExterior = usuarioGet.NumeroExterior;
                            usuario.Direccion.Colonia.Nombre = usuarioGet.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = usuarioGet.CodigoPostal;
                            usuario.Direccion.Colonia.Municipio.Nombre = usuarioGet.NombreMunicipio;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = usuarioGet.NombreEstado;

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No hay Datos/Registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetByIDEF(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var usuario = context.UsuarioGetById(idUsuario).Single();
                    if (usuario != null)
                    {
                        //result.Object = new ML.Usuario
                        ML.Usuario usuarioGet = new ML.Usuario
                        {
                            IdUsuario = Convert.ToInt32(usuario.IdUsuario),
                            Nombre = usuario.NombreUsuario,
                            ApellidoPaterno = usuario.ApellidoPaterno,
                            ApellidoMaterno = usuario.ApellidoMaterno,
                            Email = usuario.Email,
                            Password = usuario.Password,
                            FechaNacimiento = usuario.FechaNacimiento,
                            Sexo = usuario.Sexo,
                            Telefono = usuario.Telefono,
                            Celular = usuario.Celular,
                            Estatus = Convert.ToBoolean(usuario.Estatus),
                            CURP = usuario.CURP,
                            UserName = usuario.UserName,
                            Imagen = usuario.Imagen,
                            Rol = new ML.Rol(),
                            Direccion = new ML.Direccion()
                        };
                        usuarioGet.Direccion.Colonia = new ML.Colonia();
                        usuarioGet.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuarioGet.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuarioGet.Rol.IdRol = usuario.IdRol != null ? usuario.IdRol.Value : 0;
                        usuarioGet.Direccion.IdDireccion = usuario.IdDireccion != null ? usuario.IdDireccion.Value : 0;
                        usuarioGet.Direccion.Calle = usuario.calle;
                        usuarioGet.Direccion.NumeroInterior = usuario.NumeroInterior;
                        usuarioGet.Direccion.NumeroExterior = usuario.NumeroExterior;
                        usuarioGet.Direccion.Colonia.IdColonia = usuario.IdColonia != null ? usuario.IdColonia.Value : 0;
                        usuarioGet.Direccion.Colonia.Municipio.IdMunicipio = usuario.IdMunicipio != null ? usuario.IdMunicipio.Value : 0;
                        usuarioGet.Direccion.Colonia.Municipio.Estado.IdEstado = usuario.IdEstado != null ? usuario.IdEstado.Value : 0;

                        result.Object = usuarioGet;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Usuario no Encontrado";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result ExistEF(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var exist = context.UsuarioExist(idUsuario).Single();
                    int filasAfectadas = Convert.ToInt32(exist.Value);
                    if (filasAfectadas != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Usuario no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.ErrorMessage = ex.Message; result.Exception = ex;
            }
            return result;
        }

        public static ML.Result CambiarEstatusEF(int IdUsuario, bool Estatus)
        {
            ML.Result result = new Result();
            try
            {
                using(DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var filasAfectadas = context.UsuarioCambiarEstatus(IdUsuario, Estatus);
                    if(filasAfectadas != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No fue posible cambiar el estatus del usuario";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false; result.ErrorMessage = ex.Message; result.Exception = ex;
            }
            return result;
        }

        public static ML.Result CargaMasivaTXT()
        {
            ML.Result result = new ML.Result();
            string ruta = @"C:\Users\digis\Documents\Benjamin_Tecuapacho_Mendez\StreamReaderTXT.txt";
            try
            {

                StreamReader streamReader = new StreamReader(ruta);
                string linea = "";
                string cavecera =  streamReader.ReadLine();
                while((linea = streamReader.ReadLine()) != null)
                {
                    string[] columna = linea.Split('|');
                    ML.Usuario usuario = new ML.Usuario
                    {
                        Nombre = columna[0],
                        ApellidoPaterno = columna[1],
                        ApellidoMaterno = columna[2],
                        Email = columna[3],
                        FechaNacimiento = columna[4],
                        Sexo = columna[5],
                        Telefono = columna[6],
                        Celular = columna[7],
                        Estatus = Convert.ToBoolean(Convert.ToInt32(columna[8])),
                        CURP = columna[9],
                        UserName = columna[10],
                        Rol = new ML.Rol { IdRol = Convert.ToInt32(columna[11]) },
                        Direccion = new ML.Direccion{
                            Calle = columna[12],
                            NumeroInterior = columna[13],
                            NumeroExterior = columna[14],
                            Colonia = new ML.Colonia{ IdColonia = Convert.ToInt32(columna[15]) }
                        },
                        Password = columna[16]
                    };
                    BL.Usuarios.AddEF(usuario);
                }
                result.Correct = true;
            }
            catch(Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result LeerExel(string connectionString)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using(OleDbCommand cmd = new OleDbCommand(query, context))
                    {
                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count != 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in dataTable.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario
                                {
                                    Nombre = row[0].ToString(),
                                    ApellidoPaterno = row[1].ToString(),
                                    ApellidoMaterno = row[2].ToString(),
                                    Email = row[3].ToString(),
                                    Password = row[16].ToString(),
                                    FechaNacimiento = row[4].ToString(),
                                    Sexo = row[5].ToString(),
                                    Telefono = row[6].ToString(),
                                    Celular = row[7].ToString(),
                                    Estatus = (row[8].ToString() == "" || row[8].ToString() == null) ? false : Convert.ToBoolean(Convert.ToInt32(row[8].ToString())),
                                    CURP = row[9].ToString(),
                                    UserName = row[10].ToString(),
                                    Rol = new ML.Rol { IdRol = (row[11].ToString() == "" || row[11].ToString() == null) ? 0 : Convert.ToInt32(row[11].ToString()) },
                                    Direccion = new ML.Direccion
                                    {
                                        Calle = row[12].ToString(),
                                        NumeroInterior = row[13].ToString(),
                                        NumeroExterior = row[14].ToString(),
                                        Colonia = new ML.Colonia { IdColonia = (row[15].ToString() == "" || row[15].ToString() == null) ? 0 : Convert.ToInt32(row[15].ToString()) }
                                    }
                                };
                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false; result.ErrorMessage = "No hay Datos/Registros";
                        }
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false; result.ErrorMessage = ex.Message; result.Exception = ex;
            }
            return result;
        }

        public static ML.Result ValidarExel(List<object> usuarios)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            int contador = 1;
            foreach (ML.Usuario usuario in usuarios) {
                ML.ResultExel resultExel = new ML.ResultExel();
                resultExel.NumeroRegistro = contador;
                resultExel.ErrorMessage += ((usuario.UserName.Length > 50 || usuario.UserName == "" || usuario.UserName == null) ? "El nombre de usuario no puede ser vacio o mayor a 50 caracteres,": "");
                resultExel.ErrorMessage += ((usuario.Nombre.Length > 50 || usuario.Nombre == "" || usuario.Nombre == null) ? " El nombre no puede ser vacio o mayor a 50 caracteres,": "");
                resultExel.ErrorMessage += ((usuario.ApellidoPaterno.Length > 50 || usuario.ApellidoPaterno == "" || usuario.ApellidoPaterno == null) ? " El apellido paterno no puede ser vacio o mayor a 50 caracteres,": "");
                resultExel.ErrorMessage += ((usuario.ApellidoMaterno.Length > 50 || usuario.ApellidoMaterno == "" || usuario.ApellidoMaterno == null) ? " El apellido materno no puede ser vacio o mayor a 50 caracteres,": "");
                resultExel.ErrorMessage += ((usuario.Email.Length > 50 || usuario.Email == "" || usuario.Email == null) ? " El email no puede ser vacio o mayor a 50 caracteres," : "");
                resultExel.ErrorMessage += ((usuario.Password.Length > 50 || usuario.Password == "" || usuario.Password == null) ? " El password no puede ser vacio o mayor a 50 caracteres," : "");
                resultExel.ErrorMessage += ((usuario.FechaNacimiento == "" || usuario.FechaNacimiento == null) ? " La Fecha no puede ser vacia," : "");
                resultExel.ErrorMessage += ((usuario.Sexo == "" || usuario.Sexo == null) ? " El sexo no puede ser vacio" : "");
                resultExel.ErrorMessage += ((usuario.Telefono.Length > 20 || usuario.Telefono == "" || usuario.Telefono == null) ? " El telefono no puede ser vacio o mayor a 20 caracteres," : "");
                resultExel.ErrorMessage += ((usuario.Celular.Length > 20 || usuario.Celular == "" || usuario.Celular == null) ? " El celular no puede ser vacio o mayor a 20 caracteres," : "");
                resultExel.ErrorMessage += ((usuario.Estatus == null) ? " El estatus no puede ser vacio" : "");
                resultExel.ErrorMessage += ((usuario.CURP.Length > 20 || usuario.CURP == "" || usuario.CURP == null) ? " El CURP no puede ser vacio o mayor a 20 caracteres," : "");
                resultExel.ErrorMessage += ((usuario.Rol.IdRol == 0 || usuario.Rol.IdRol == null) ? " El id de la colonia no puede ser vacia o 0," : "");
                resultExel.ErrorMessage += ((usuario.Direccion.Calle.Length > 50 || usuario.Direccion.Calle == "" || usuario.Direccion.Calle == null) ? " La calle no puede ser vacia o mayor a 50 caracteres," : "");
                resultExel.ErrorMessage += ((usuario.Direccion.NumeroInterior.Length > 20 || usuario.Direccion.NumeroInterior == "" || usuario.Direccion.NumeroInterior == null) ? " El numero interior no puede ser vacio o mayor a 20 caracteres," : "");
                resultExel.ErrorMessage += ((usuario.Direccion.NumeroExterior.Length > 20 || usuario.Direccion.NumeroExterior == "" || usuario.Direccion.NumeroExterior == null) ? " El numero exterior no puede ser vacio o mayor a 20 caracteres," : "");
                resultExel.ErrorMessage += ((usuario.Direccion.Colonia.IdColonia == 0 || usuario.Direccion.Colonia.IdColonia == null) ? " El id de la colonia no puede ser vacia o 0," : "");
                if(resultExel.ErrorMessage != "")
                {
                    result.Objects.Add(resultExel);
                }
                contador++;
            }
            return result;
        }

        //Usando LINQ
        public static ML.Result AddLQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    DateTime fecha = DateTime.ParseExact(usuario.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DL_EF.Usuario UsuarioInsert = new DL_EF.Usuario{
                        UserName = usuario.UserName,
                        Nombre = usuario.Nombre,
                        ApellidoPaterno = usuario.ApellidoPaterno,
                        ApellidoMaterno = usuario.ApellidoMaterno,
                        Email = usuario.Email,
                        Password = usuario.Password,
                        FechaNacimiento = fecha,
                        Sexo = usuario.Sexo,
                        Telefono = usuario.Telefono,
                        Celular = usuario.Celular,
                        Estatus = usuario.Estatus,
                        CURP = usuario.CURP
                    };
                    context.Usuarios.Add(UsuarioInsert);
                    int filasAfectadas = context.SaveChanges();
                    if (filasAfectadas != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Error al registrar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateLQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var usuarioSelect = (from user in context.Usuarios
                                      where user.IdUsuario == usuario.IdUsuario
                                      select user).Single();
                    if (usuarioSelect != null)
                    {
                        DateTime fecha = DateTime.ParseExact(usuario.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        usuarioSelect.UserName = usuario.UserName;
                        usuarioSelect.Nombre = usuario.Nombre;
                        usuarioSelect.ApellidoPaterno = usuario.ApellidoPaterno;
                        usuarioSelect.ApellidoMaterno = usuario.ApellidoMaterno;
                        usuarioSelect.Email = usuario.Email;
                        usuarioSelect.Password = usuario.Password;
                        usuarioSelect.FechaNacimiento = fecha;
                        usuarioSelect.Sexo = usuario.Sexo;
                        usuarioSelect.Telefono = usuario.Telefono;
                        usuarioSelect.Celular = usuario.Celular;
                        usuarioSelect.Estatus = usuario.Estatus;
                        usuarioSelect.CURP = usuario.CURP;
                        int filasAfectadas = context.SaveChanges();
                        if (filasAfectadas != 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false; result.ErrorMessage = "Error al actualizar el usuario";
                        }
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Usuario no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result DeleteLQ(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var usuarioDelete = (from usuario in context.Usuarios
                                  where usuario.IdUsuario == idUsuario
                                  select usuario).SingleOrDefault();
                    if (usuarioDelete != null)
                    {
                        context.Usuarios.Remove(usuarioDelete);
                        int filasAfectadas = context.SaveChanges();
                        if (filasAfectadas != 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false; result.ErrorMessage = "Error al eliminar el usuario";
                        }
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Usuario no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAllLQ()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var usuarios = (from usuario in context.Usuarios
                                      select new
                                      {
                                          IdUsuario = usuario.IdUsuario,
                                          UserName = usuario.UserName,
                                          Nombre = usuario.Nombre,
                                          ApellidoPaterno = usuario.ApellidoPaterno,
                                          ApellidoMaterno = usuario.ApellidoMaterno,
                                          Email = usuario.Email,
                                          Password = usuario.Password,
                                          FechaNacimiento = usuario.FechaNacimiento,
                                          Sexo = usuario.Sexo,
                                          Telefono = usuario.Telefono,
                                          Celular = usuario.Celular,
                                          Estatus = usuario.Estatus,
                                          CURP = usuario.CURP
                                      }).ToList();
                    if (usuarios.Count() != 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var usuarioGet in usuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = Convert.ToInt32(usuarioGet.IdUsuario);
                            usuario.Nombre = usuarioGet.Nombre;
                            usuario.ApellidoPaterno = usuarioGet.ApellidoPaterno;
                            usuario.ApellidoMaterno = usuarioGet.ApellidoMaterno;
                            usuario.Email = usuarioGet.Email;
                            usuario.Password = usuarioGet.Password;
                            usuario.FechaNacimiento = usuarioGet.FechaNacimiento.ToString("dd/MM/yyyy");
                            usuario.Sexo = usuarioGet.Sexo;
                            usuario.Telefono = usuarioGet.Telefono;
                            usuario.Celular = usuarioGet.Celular;
                            usuario.Estatus = usuarioGet.Estatus;
                            usuario.CURP = usuarioGet.CURP;
                            usuario.UserName = usuarioGet.UserName;

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "No hay Datos/Registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetByIDLQ(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    var usuarioGet = (from usuario in context.Usuarios
                                  where usuario.IdUsuario == idUsuario
                                  select new
                                  {
                                      IdUsuario = usuario.IdUsuario,
                                      UserName = usuario.UserName,
                                      Nombre = usuario.Nombre,
                                      ApellidoPaterno = usuario.ApellidoPaterno,
                                      ApellidoMaterno = usuario.ApellidoMaterno,
                                      Email = usuario.Email,
                                      Password = usuario.Password,
                                      FechaNacimiento = usuario.FechaNacimiento,
                                      Sexo = usuario.Sexo,
                                      Telefono = usuario.Telefono,
                                      Celular = usuario.Celular,
                                      Estatus = usuario.Estatus,
                                      CURP = usuario.CURP
                                  }).SingleOrDefault();
                    if (usuarioGet != null)
                    {
                        var usuario = usuarioGet;
                        result.Object = new ML.Usuario
                        {
                            IdUsuario = Convert.ToInt32(usuario.IdUsuario),
                            Nombre = usuario.Nombre,
                            ApellidoPaterno = usuario.ApellidoPaterno,
                            ApellidoMaterno = usuario.ApellidoMaterno,
                            Email = usuario.Email,
                            Password = usuario.Password,
                            FechaNacimiento = usuarioGet.FechaNacimiento.ToString("dd/MM/yyyy"),
                            Sexo = usuario.Sexo,
                            Telefono = usuario.Telefono,
                            Celular = usuario.Celular,
                            Estatus = usuario.Estatus,
                            CURP = usuario.CURP,
                            UserName = usuario.UserName
                        };
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Usuario no Encontrado";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result ExistLQ(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.BTecuapachoProgramacionNCapasEntities context = new DL_EF.BTecuapachoProgramacionNCapasEntities())
                {
                    int exist = context.Usuarios.Count(usuario => usuario.IdUsuario == idUsuario);
                    if (exist != 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; result.ErrorMessage = "Usuario no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; result.ErrorMessage = ex.Message; result.Exception = ex;
            }
            return result;
        }
    }
}
