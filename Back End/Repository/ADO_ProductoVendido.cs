using static Hola.Controllers.ProductoVendidoController;
using Hola.Models;
using Hola.Repository;
using System.Data.SqlClient;
using System.Data;

namespace Hola.Repository
{
    public class ADO_ProductoVendido
    {
        public static List<ProductoVendido> GetProductoV(int id)
        {
            var listaPV = new List<ProductoVendido>();
            var query = "select ProductoVendido.Id, ProductoVendido.IdProducto, ProductoVendido.IdVenta, ProductoVendido.Stock, Producto.IdUsuario from ProductoVendido, Producto WHERE Producto.IdUsuario = @IdUsuario";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand comando = new SqlCommand(query, connection))
                {
                    comando.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.Int)).Value = id;

                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var pv = new ProductoVendido();

                            pv.Id = Convert.ToInt32(dr.GetValue(0));
                            pv.Stock = Convert.ToInt32(dr.GetValue(1));
                            pv.IdProducto = Convert.ToInt32(dr.GetValue(2));
                            pv.IdVenta = Convert.ToInt32(dr.GetValue(3));


                            listaPV.Add(pv);

                        }
                        dr.Close();

                    }
                }

            }
            return listaPV;
        }
        public static void EliminarProductoV(int id)
        {
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "DELETE FROM productoVendido where id = @IdUs";
                var param = new SqlParameter();
                param.ParameterName = "IdUs";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = id;

                cmd2.Parameters.Add(param);
                cmd2.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

