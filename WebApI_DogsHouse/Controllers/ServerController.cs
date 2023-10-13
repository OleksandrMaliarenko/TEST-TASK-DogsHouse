using Microsoft.AspNetCore.Mvc;

namespace WebApI_DogsHouse.Controllers
{
    public class ServerController : ControllerBase
    {
        [HttpGet]
        [Route("ping")]
        public async Task<IActionResult> Ping()
        {
            return Ok("Dogshouseservice.Version1.0.1");
        }
    }
}
