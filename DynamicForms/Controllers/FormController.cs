using DynamicForms.Interfaces;
using DynamicForms.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DynamicForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController(IFormRepository repository) : ControllerBase
    {
        private readonly IFormRepository _repository = repository;
        [HttpGet("{optionId}")]
        public async Task<IActionResult> GetAsync(int optionId)
        {
            try
            {
                var result = await _repository.GetAsync(optionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
