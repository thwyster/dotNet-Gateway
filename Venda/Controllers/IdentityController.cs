using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Venda.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(100); // simulate latency
            var items = new List<Produto>()
            {
                new Produto { ID = 1, Name = "Cog", Shape = "Square" },
                new Produto { ID = 2, Name = "Gear", Shape = "Round" },
                new Produto { ID = 3, Name = "Sprocket", Shape = "Octagonal" },
                new Produto { ID = 4, Name = "Pinion", Shape = "Triangular" }
            };

            return Ok(items);
        }
    }
}
