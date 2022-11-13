namespace Hola.Models
{
    public class ListaVenta : Venta 
    {
        public List<ProductoVendido> Productos { get; set; }
    }
}
