using System.Data.SqlClient;
using System.Data;
using Hola.Models;
using Hola.Controllers;
namespace Hola.Repository
{
    public class ADO_Venta
    {
        public static (List<Venta>, List<Producto>) GetVenta()
        {
            var listaProducto = new List<Producto>();
            var listaVenta = new List<Venta>();
            var query = "SELECT Venta.Id,Venta.Comentarios,Venta.IdUsuario, ProductoVendido.IdProducto, ProductoVendido.IdVenta, Producto.Id, Producto.Descripciones from Venta,ProductoVendido, Producto WHERE ProductoVendido.IdVenta = Venta.Id AND ProductoVendido.IdProducto = Producto.Id";
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
                            var venta = new Venta();

                            var prod = new Producto();

                            venta.Id = Convert.ToInt32(dr.GetValue(0));
                            venta.Comentarios = dr.GetValue(1).ToString();
                            venta.IdUsuario = Convert.ToInt32(dr.GetValue(2));
                            prod.Id = Convert.ToInt32(dr.GetValue(5));
                            prod.Descripciones = dr.GetValue(6).ToString();

                            listaVenta.Add(venta);
                            listaProducto.Add(prod);
                        }
                        dr.Close();

                    }
                }

            }
            return (listaVenta, listaProducto);

        }
        public static void EliminarVenta(Venta venta)
        {
            var listaPV = new List<ProductoVendido>();
            string query = "SELECT * FROM ProductoVendido Where IdProducto = @IdVn";
            string query2 = "UPDATE Producto SET Stock = Stock + @PvStock " +
    "WHERE Id = @IdProducto";
            string query3 = "DELETE FROM ProductoVendido where IdVenta = @IdVn";
            string query4 = "DELETE FROM venta WHERE Id = @Idvnt";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd3 = new SqlCommand(query, connection))
                {
                    var param = new SqlParameter();
                    param.ParameterName = "IdVn";
                    param.SqlDbType = SqlDbType.BigInt;
                    param.Value = venta.Id;
                    cmd3.Parameters.Add(param);

                    using (SqlDataReader dr = cmd3.ExecuteReader())
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

                using SqlCommand cmd = new SqlCommand(query2, connection);
                {

                    foreach (ProductoVendido pv in listaPV)
                    {
                        cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.Int)).Value = pv.IdProducto;
                        cmd.Parameters.Add(new SqlParameter("PvStock", SqlDbType.Int)).Value = pv.Stock;
                        cmd.ExecuteNonQuery();
                    }

                }

                using SqlCommand cmd2 = new SqlCommand(query3, connection);
                {
                    var param = new SqlParameter();
                    param.ParameterName = "IdVn";
                    param.SqlDbType = SqlDbType.BigInt;
                    param.Value = venta.Id;

                    cmd2.Parameters.Add(param);
                    cmd2.ExecuteNonQuery();

                }
             
                using (SqlCommand cmd4 = new SqlCommand(query4, connection))
                {
                    var param = new SqlParameter();
                    param.ParameterName = "Idvnt";
                    param.SqlDbType = SqlDbType.BigInt;
                    param.Value = venta.Id;

                    cmd2.Parameters.Add(param);
                    cmd2.ExecuteNonQuery();
 
                } 
                connection.Close();
            }

        }
        public static void CargarVenta(ListaVenta vn)
        {
            long IdVenta;
            string query = "Insert into Venta (Comentarios, idUsuario) OUTPUT INSERTED.id values(@Comentarios, @idUsuario) SELECT SCOPE_IDENTITY()";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using SqlCommand cmd3 = new SqlCommand(query, connection);
                {
                    var paramComen = new SqlParameter();
                    paramComen.ParameterName = "Comentarios";
                    paramComen.SqlDbType = SqlDbType.VarChar;
                    paramComen.Value = vn.Comentarios;

                    var paramIdUsu = new SqlParameter();
                    paramIdUsu.ParameterName = "IdUsuario";
                    paramIdUsu.SqlDbType = SqlDbType.VarChar;
                    paramIdUsu.Value = vn.IdUsuario;

                    cmd3.Parameters.Add(paramComen);
                    cmd3.Parameters.Add(paramIdUsu);
                    IdVenta = Convert.ToInt64(cmd3.ExecuteScalar());

                }

                foreach (ProductoVendido product in vn.Productos)
                {
                    var cmd = new SqlCommand("INSERT INTO ProductoVendido (Stock,IdProducto,IdVenta)  VALUES   (@Stock,@IdProducto,@IdVenta) ", connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = product.Stock;
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt)).Value = product.IdProducto;
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt)).Value = IdVenta;
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("UPDATE Producto SET Stock = Stock - @Stock WHERE Id = @IdProducto", connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = product.Stock;
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt)).Value = product.IdProducto;
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

    }
}
