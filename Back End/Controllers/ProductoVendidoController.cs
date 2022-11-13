using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hola.Models;
using Hola.Repository;

namespace Hola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        public List<ProductoVendido> Get(int id)
        {
            return ADO_ProductoVendido.GetProductoV(id);
        }

        [HttpDelete]
        public void Eliminar([FromBody] int id)
        {
            ADO_ProductoVendido.EliminarProductoV(id);
        }
    }
}
