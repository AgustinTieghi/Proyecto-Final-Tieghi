using Hola.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using Hola.Models;
using Hola.Repository;

namespace Hola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        [HttpGet("{nombreUsuario}")]
        public Usuario Get(string nombreUsuario)
        {
            return ADO_Usuario.TraerUsuario(nombreUsuario);
        }

        [HttpGet("{nombreUsuario}/{contraseña}")]
        public Usuario Get(string nombreusuario, string contraseña)
        {
            return ADO_Usuario.InicioSesion(nombreusuario, contraseña);
        }

        [HttpDelete]
        public void Eliminar([FromBody] int id)
        {
            ADO_Usuario.EliminarUsuario(id);
        }

        [HttpPut]
        public void Modificar([FromBody] Usuario us)
        {
            ADO_Usuario.ModificarUsuario(us);
        }

        [HttpPost]
        public void Agregar([FromBody] Usuario us)
        {
            ADO_Usuario.AgregarUsuario(us);
        }

    }
}
