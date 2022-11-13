using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NombreController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Proyecto Final Tieghi";
        }
    }
}
