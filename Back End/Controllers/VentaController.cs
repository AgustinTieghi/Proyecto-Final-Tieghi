using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using Hola.Models;
using Hola.Repository;

namespace Hola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        [HttpGet("{idUsuario}")]
        public (List<Venta>, List<Producto>) Get()
        {
            return ADO_Venta.GetVenta();
        }

        [HttpDelete]
        public void Eliminar([FromBody] Venta venta)
        {
            ADO_Venta.EliminarVenta(venta);
        }

        [HttpPost("{idUsuario}")]
        public void Agregar([FromBody] ListaVenta vn)
        {
            ADO_Venta.CargarVenta(vn);
        }
    }
}

