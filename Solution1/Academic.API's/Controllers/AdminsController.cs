using Academic.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Academic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            return Ok();
        }
    }
}
