using static Hola.Controllers.UsuarioController;
using Hola.Models;
using Hola.Repository;
using System.Data;
using System.Data.SqlClient;

namespace Hola.Repository
{
    public class ADO_Usuario
    {
        public static Usuario TraerUsuario(string Username)
        {
            var query = @"SELECT * FROM usuario where NombreUsuario = @username";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, connection))
                {
                    var param = new SqlParameter();
                    param.ParameterName = "username";
                    param.SqlDbType = SqlDbType.VarChar;
                    param.Value = Username;
                    comando.Parameters.Add(param);
                    connection.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                       if (dr.Read())
                       {
                            var usuario = new Usuario();

                            usuario.Id = Convert.ToInt32(dr.GetValue(0));
                            usuario.Nombre = dr.GetValue(1).ToString();
                            usuario.Apellido = dr.GetValue(2).ToString();
                            usuario.NombreUsuario = dr.GetValue(3).ToString();
                            usuario.Contraseña = dr.GetValue(4).ToString();
                            usuario.Mail = dr.GetValue(5).ToString();

                            return usuario;
                        }
                       else
                        {
                            throw new Exception("Error");
                        }
                    }
                }

            }


        }
        public static void EliminarUsuario(int id)
        {
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "DELETE FROM usuario where id = @IdUs";
                var param = new SqlParameter();
                param.ParameterName = "IdUs";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = id;

                cmd2.Parameters.Add(param);
                cmd2.ExecuteNonQuery();
                connection.Close();
            }
        }
        public static void ModificarUsuario(Usuario us)
        {
            string query = "UPDATE Usuario SET Nombre = @NombreUsu, Apellido = @Apellidos, NombreUsuario = @NombreUsuarioUsu, Contraseña = @ContraseñaUsu, Mail = @MailUsu " + " WHERE Id = @IdUsu";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                var paramID = new SqlParameter();
                paramID.ParameterName = "IdUsu";
                paramID.SqlDbType = SqlDbType.BigInt;
                paramID.Value = us.Id;

                var paramNombre = new SqlParameter();
                paramNombre.ParameterName = "NombreUsu";
                paramNombre.SqlDbType = SqlDbType.VarChar;
                paramNombre.Value = us.Nombre;

                var paramApellido = new SqlParameter();
                paramApellido.ParameterName = "Apellidos";
                paramApellido.SqlDbType = SqlDbType.VarChar;
                paramApellido.Value = us.Apellido;

                var paramUsername = new SqlParameter();
                paramUsername.ParameterName = "NombreUsuarioUsu";
                paramUsername.SqlDbType = SqlDbType.VarChar;
                paramUsername.Value = us.NombreUsuario;

                var paramPass = new SqlParameter();
                paramPass.ParameterName = "ContraseñaUsu";
                paramPass.SqlDbType = SqlDbType.VarChar;
                paramPass.Value = us.Contraseña;

                var paramMail = new SqlParameter();
                paramMail.ParameterName = "MailUsu";
                paramMail.SqlDbType = SqlDbType.VarChar;
                paramMail.Value = us.Mail;

                cmd.Parameters.Add(paramID);
                cmd.Parameters.Add(paramNombre);
                cmd.Parameters.Add(paramApellido);
                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramPass);
                cmd.Parameters.Add(paramMail);

                cmd.ExecuteNonQuery();
                connection.Close();
            }

        }
        public static bool AgregarUsuario(Usuario usuario)
        {
            bool alta = false;
            Usuario usuarioRepetido = TraerUsuario(usuario.NombreUsuario);

            if (usuario.NombreUsuario == null ||
                usuario.NombreUsuario.Trim() == "" ||
                usuario.Contraseña == null ||
                usuario.Contraseña.Trim() == "" ||
                usuario.Nombre == null ||
                usuario.Nombre.Trim() == "" ||
                usuario.Apellido == null ||
                usuario.Apellido.Trim() == "")
            {
                return alta;
                throw new Exception("Faltan datos obligatorios");
            }
            else if (usuarioRepetido.Id != 0)
            {
                return alta;
                throw new Exception("El nombre de usuario ya existe");
            }
            else
            {
                string query = "Insert into Usuario  (Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                "Values (@NombreUsu, @Apellidos, @NombreUsuarioUsu, @ContraseñaUsu, @MailUsu)";
                string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd3 = new SqlCommand(query, connection);

                    var paramNombre = new SqlParameter();
                    paramNombre.ParameterName = "NombreUsu";
                    paramNombre.SqlDbType = SqlDbType.VarChar;
                    paramNombre.Value = usuario.Nombre;

                    var paramApellido = new SqlParameter();
                    paramApellido.ParameterName = "Apellidos";
                    paramApellido.SqlDbType = SqlDbType.VarChar;
                    paramApellido.Value = usuario.Apellido;

                    var paramUsername = new SqlParameter();
                    paramUsername.ParameterName = "NombreUsuarioUsu";
                    paramUsername.SqlDbType = SqlDbType.VarChar;
                    paramUsername.Value = usuario.NombreUsuario;

                    var paramPass = new SqlParameter();
                    paramPass.ParameterName = "ContraseñaUsu";
                    paramPass.SqlDbType = SqlDbType.VarChar;
                    paramPass.Value = usuario.Contraseña;

                    var paramMail = new SqlParameter();
                    paramMail.ParameterName = "MailUsu";
                    paramMail.SqlDbType = SqlDbType.VarChar;
                    paramMail.Value = usuario.Mail;

                    cmd3.Parameters.Add(paramNombre);
                    cmd3.Parameters.Add(paramApellido);
                    cmd3.Parameters.Add(paramUsername);
                    cmd3.Parameters.Add(paramPass);
                    cmd3.Parameters.Add(paramMail);

                    cmd3.ExecuteReader();
                    alta = true;
                    return alta;
                    connection.Close();
                }
            }
        }
        public static Usuario InicioSesion(string Username, string pass)
        {

            var query = @"SELECT * FROM usuario where NombreUsuario = @Username AND Contraseña = @pass";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, connection))
                {
                    var parametro = new SqlParameter();
                    parametro.ParameterName = "Username";
                    parametro.SqlDbType = SqlDbType.VarChar;
                    parametro.Value = Username;
                    comando.Parameters.Add(parametro);

                    var parametro2 = new SqlParameter();
                    parametro2.ParameterName = "pass";
                    parametro2.SqlDbType = SqlDbType.VarChar;
                    parametro2.Value = pass;
                    comando.Parameters.Add(parametro2);

                    connection.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            var usuario = new Usuario();

                            usuario.Id = Convert.ToInt32(dr.GetValue(0));
                            usuario.Nombre = dr.GetValue(1).ToString();
                            usuario.Apellido = dr.GetValue(2).ToString();
                            usuario.NombreUsuario = dr.GetValue(3).ToString();
                            usuario.Contraseña = dr.GetValue(4).ToString();
                            usuario.Mail = dr.GetValue(5).ToString();

                            Console.WriteLine("id = " + usuario.Id);
                            Console.WriteLine("Nombre = " + usuario.Nombre);
                            Console.WriteLine("Apellido = " + usuario.Apellido);
                            Console.WriteLine("Nombre de Usuario = " + usuario.NombreUsuario);
                            Console.WriteLine("Contraseña = " + usuario.Contraseña);
                            Console.WriteLine("Mail = " + usuario.Mail);
                            Console.WriteLine("--------------");

                            return usuario;

                        }
                        else
                        {
                            var usuario = new Usuario();

                            Console.WriteLine("id = " + usuario.Id);
                            Console.WriteLine("Nombre = " + usuario.Nombre);
                            Console.WriteLine("Apellido = " + usuario.Apellido);
                            Console.WriteLine("Nombre de Usuario = " + usuario.NombreUsuario);
                            Console.WriteLine("Contraseña = " + usuario.Contraseña);
                            Console.WriteLine("Mail = " + usuario.Mail);
                            Console.WriteLine("--------------");

                            return usuario;
                        }

                    }
                }
            }




        }
    }
}


