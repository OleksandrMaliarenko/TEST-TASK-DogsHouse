using BLL_DogsHouse.Interfaces;
using BLL_DogsHouse.Models.Queries;
using BLL_DogsHouse.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace WebApI_DogsHouse.Controllers
{
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly IDogService _dogService;

        public DogController(IDogService dogService)
        {
            _dogService = dogService;
        }

        [HttpGet]
        [Route("dogs")]
        public async Task<IActionResult> Get([FromQuery] DogQuery dogQuery)
        {
            var result = await _dogService.Get(dogQuery);

            return Ok(result);
        }

        [HttpPost]
        [Route("dog")]
        public async Task<IActionResult> Create(DogRequest dogRequest)
        {
            var result = await _dogService.Create(dogRequest);

            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }
    }
}
