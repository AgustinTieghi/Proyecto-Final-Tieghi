using static Hola.Controllers.ProductoController;
using System.Data;
using System.Data.SqlClient;
using static Hola.Controllers.UsuarioController;
using Hola.Models;
using Hola.Repository;

namespace Hola.Repository
{
    public class ADO_Producto
    {
        public static List<Producto> GetProductos()
        {
            var listaProductos = new List<Producto>();
            var query = @"SELECT * FROM producto";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var produc = new Producto();

                            produc.Id = Convert.ToInt32(dr.GetValue(0));
                            produc.Descripciones = dr.GetValue(1).ToString();
                            produc.Costo = Convert.ToInt32(dr.GetValue(2));
                            produc.PrecioVenta = Convert.ToInt32(dr.GetValue(3));
                            produc.Stock = Convert.ToInt32(dr.GetValue(4));
                            produc.IdUsuario = Convert.ToInt32(dr.GetValue(5));

                            listaProductos.Add(produc);

                        }
                        dr.Close();

                    }
                }

            }
            return listaProductos;

        }

        public static void EliminarProducto(int id)
        {
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "DELETE FROM producto where id = @IdUs";
                var param = new SqlParameter();
                param.ParameterName = "IdUs";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = id;

                cmd2.Parameters.Add(param);
                cmd2.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void ModificarProducto(Producto pr)
        {
            string query = "UPDATE Producto SET Descripcion = @DescripPro, Costo = @CostoPro, PrecioVenta = @PrecioVentaPro, Stock = @StockPro, IdUsuario = @IdUsuPro " + " WHERE Id = @IdUsu";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                var paramID = new SqlParameter();
                paramID.ParameterName = "IdUsu";
                paramID.SqlDbType = SqlDbType.BigInt;
                paramID.Value = pr.Id;

                var paramDscrp = new SqlParameter();
                paramDscrp.ParameterName = "DescripPro";
                paramDscrp.SqlDbType = SqlDbType.VarChar;
                paramDscrp.Value = pr.Descripciones;

                var paramCosto = new SqlParameter();
                paramCosto.ParameterName = "CostoPro";
                paramCosto.SqlDbType = SqlDbType.Int;
                paramCosto.Value = pr.Costo;

                var paramPrecioVenta = new SqlParameter();
                paramPrecioVenta.ParameterName = "PrecioVentaPro";
                paramPrecioVenta.SqlDbType = SqlDbType.Int;
                paramPrecioVenta.Value = pr.PrecioVenta;

                var paramStock = new SqlParameter();
                paramStock.ParameterName = "StockPro";
                paramStock.SqlDbType = SqlDbType.VarChar;
                paramStock.Value = pr.Stock;

                var paramIdUsu = new SqlParameter();
                paramIdUsu.ParameterName = "IdUsuPro";
                paramIdUsu.SqlDbType = SqlDbType.VarChar;
                paramIdUsu.Value = pr.IdUsuario;

                cmd.Parameters.Add(paramID);
                cmd.Parameters.Add(paramDscrp);
                cmd.Parameters.Add(paramCosto);
                cmd.Parameters.Add(paramPrecioVenta);
                cmd.Parameters.Add(paramStock);
                cmd.Parameters.Add(paramIdUsu);

                cmd.ExecuteNonQuery();
                connection.Close();
            }

        }

        public static Producto AgregarProducto(Producto pr)
        {
            string query = "Insert into Producto (Descripciones, Costo, PrecioVenta, Stock ,IdUsuario) " +
                "Values (@IdUsu, @DescripPro, @CostoPro, @PreciVentaPro, @StockPro, IdUsuPro)";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                var paramID = new SqlParameter();
                paramID.ParameterName = "IdUsu";
                paramID.SqlDbType = SqlDbType.BigInt;
                paramID.Value = pr.Id;

                var paramDscrp = new SqlParameter();
                paramDscrp.ParameterName = "DescripPro";
                paramDscrp.SqlDbType = SqlDbType.VarChar;
                paramDscrp.Value = pr.Descripciones;

                var paramCosto = new SqlParameter();
                paramCosto.ParameterName = "CostoPro";
                paramCosto.SqlDbType = SqlDbType.Int;
                paramCosto.Value = pr.Costo;

                var paramPrecioVenta = new SqlParameter();
                paramPrecioVenta.ParameterName = "PrecioVentaPro";
                paramPrecioVenta.SqlDbType = SqlDbType.Int;
                paramPrecioVenta.Value = pr.PrecioVenta;

                var paramStock = new SqlParameter();
                paramStock.ParameterName = "StockPro";
                paramStock.SqlDbType = SqlDbType.VarChar;
                paramStock.Value = pr.Stock;

                var paramIdUsu = new SqlParameter();
                paramIdUsu.ParameterName = "IdUsuPro";
                paramIdUsu.SqlDbType = SqlDbType.VarChar;
                paramIdUsu.Value = pr.IdUsuario;

                cmd.Parameters.Add(paramID);
                cmd.Parameters.Add(paramDscrp);
                cmd.Parameters.Add(paramCosto);
                cmd.Parameters.Add(paramPrecioVenta);
                cmd.Parameters.Add(paramStock);
                cmd.Parameters.Add(paramIdUsu);

                cmd.ExecuteNonQuery();
                return pr;
                connection.Close();

            }
        }
    }
}

