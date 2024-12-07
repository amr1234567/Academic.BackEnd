using Academic.Core.Abstractions;
using Academic.Services.Abstractions;
using Academic.Services.Models.Outputs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleServices _moduleServices;

        public ModulesController(IModuleServices moduleServices)
        {
            _moduleServices = moduleServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModules([FromQuery] int page = 1
            , [FromQuery] int size = 10)
        {
            var modules = await _moduleServices.GetAllModules(page, size);
            return Ok(modules);
        }

        [HttpGet("path/{pathId}")]
        public async Task<IActionResult> GetModulesInPath(int pathId, [FromQuery] int page = 1
            , [FromQuery] int size = 10)
        {
            var modules = await _moduleServices.GetAllModulesInPath(pathId, page, size);

            return Ok(modules);
        }

        [HttpGet("{moduleId}")]
        public async Task<IActionResult> GetModuleById(int moduleId)
        {
            var module = await _moduleServices.GetModuleById(moduleId);

            if(module == null) 
                return NotFound();

            return Ok(module);
        }

        [HttpGet("sections/{sectionId}")]
        public async Task<IActionResult> GetSectionById(int sectionId)
        {
            var section = await _moduleServices.GetSectionById(sectionId);

            if(section == null)
                return NotFound();

            return Ok(section);
        }

        [HttpGet("{moduleId}/sections")]
        public async Task<IActionResult> GetSectionsInModule(int moduleId,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var sections = await _moduleServices.GetSectionsInModule(moduleId, page, size);
            return Ok(sections);
        }

    }
}
