using DL;
using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
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
                bool primeraLinea = true;
                result.Objects = new List<object>();
                while((linea = streamReader.ReadLine()) != null)
                {
                    if (primeraLinea)
                    {
                        primeraLinea = false; continue;
                    }
                    string[] columna = linea.Split('\t');
                    if (columna.Length > 20)
                    {
                        ML.Usuario usuario = new ML.Usuario
                        {
                            IdUsuario = int.Parse(columna[0]),
                            Nombre = columna[1],
                            ApellidoPaterno = columna[2],
                            ApellidoMaterno = columna[3],
                            Email = columna[4],
                            FechaNacimiento = columna[5],
                            Sexo = columna[6],
                            Telefono = columna[7],
                            Celular = columna[8],
                            Estatus = bool.Parse(columna[9]),
                            CURP = columna[10],
                            UserName = columna[11],
                            Rol = new ML.Rol{ Nombre = columna[12] },
                            Direccion = new ML.Direccion
                            {
                                Calle = columna[13],
                                NumeroInterior = columna[14],
                                NumeroExterior = columna[15],
                                Colonia = new ML.Colonia
                                {
                                    Nombre = columna[16],
                                    CodigoPostal = columna[19],
                                    Municipio = new ML.Municipio { Nombre = columna[18] }
                                }
                            }
                        };
                    }
                    //Console.WriteLine(linea);
                }
            }catch(Exception ex)
            {
                result.Correct = false; result.Exception = ex; result.ErrorMessage = ex.Message;
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
