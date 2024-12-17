using Academic.API.Models;
using Academic.Services.Abstractions;
using Academic.Services.Models.Inputs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Academic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController(IAdminServices adminServices, IIdentityUserServices userServices, IMapper mapper) 
        : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await userServices.SignIn(model.Email, model.Password);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return Ok(result.Value);
        }

        //[NonAction]
        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin(RegisterAdminModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var admin = mapper.Map<CreateAdminModel>(model);
            var result = await adminServices.GenerateAdmin(admin);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return Created();
        }

        [HttpPost("create-instructor")]
        public async Task<IActionResult> CreateInstructor(RegisterInstructorModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var instructor = mapper.Map<CreateInstructorModel>(model);
            var result = await adminServices.GenerateInstructor(instructor);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return Ok(result.Value);
        }
    }
}
