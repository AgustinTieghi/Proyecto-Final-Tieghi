using Hola.Repository;
using Hola.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Hola.Controllers.UsuarioController;

namespace Hola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        [HttpGet("{idUsuario}")]
        public List<Producto> Get()
        {
            return ADO_Producto.GetProductos();
        }

        [HttpDelete("{idProducto}")]
        public void Eliminar([FromBody] int id)
        {
            ADO_Producto.EliminarProducto(id);
        }

        [HttpPut]
        public void Modificar([FromBody] Producto pr)
        {
            ADO_Producto.ModificarProducto(pr);
        }

        [HttpPost]
        public void Agregar([FromBody] Producto pr)
        {
            ADO_Producto.AgregarProducto(pr);
        }
    }
}

