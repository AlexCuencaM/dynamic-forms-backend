using DynamicOptions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynamicOptions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController(IOptionsRepository repository) : ControllerBase
    {
        private readonly IOptionsRepository _repository = repository;
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _repository.GetAsync();
                return Ok(result);
            }
            catch(Exception e) { 
                return BadRequest(e.Message);
            }

        }
    }
}
