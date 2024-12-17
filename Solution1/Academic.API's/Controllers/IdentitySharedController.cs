using Academic.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentitySharedController(IModuleServices _moduleServices) : ControllerBase
    {
        [HttpGet("modules/{moduleId}")]
        public async Task<IActionResult> GetModuleById(int moduleId)
        {
            var module = await _moduleServices.GetModuleById(moduleId);

            if (module == null)
                return NotFound();

            return Ok(module);
        }

        [HttpGet("modules/sections/{sectionId}")]
        public async Task<IActionResult> GetSectionById(int sectionId)
        {
            var section = await _moduleServices.GetSectionById(sectionId);

            if (section == null)
                return NotFound();

            return Ok(section);
        }

        [HttpGet("modules/{moduleId}/sections")]
        public async Task<IActionResult> GetSectionsInModule(int moduleId,
           [FromQuery] int page = 1,
           [FromQuery] int size = 10)
        {
            var sections = await _moduleServices.GetSectionsInModule(moduleId, page, size);
            return Ok(sections);
        }
    }
}
