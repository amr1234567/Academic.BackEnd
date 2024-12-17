using Academic.API.Models;
using Academic.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Academic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousController(IModuleServices _moduleServices) : ControllerBase
    {

        [HttpGet("modules")]
        public async Task<IActionResult> GetAllModules([FromQuery] PagingQuery? pagingQuery)
        {
            var modules = await _moduleServices.GetAllModules(pagingQuery?.Page ?? 1, pagingQuery?.Size ?? 10);
            return Ok(modules);
        }

        [HttpGet("path/{pathId}/modules")]
        public async Task<IActionResult> GetModulesInPath(int pathId, [FromQuery] PagingQuery? pagingQuery)
        {
            var modules = await _moduleServices.GetAllModulesInPath(pathId, pagingQuery?.Page ?? 1, pagingQuery?.Size ?? 10);

            return Ok(modules);
        }
    }
}
