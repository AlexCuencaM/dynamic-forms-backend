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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _repository.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(FormDTO form)
        {
            try
            {
                var result = await _repository.PostAsync(form);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync(FormDTO form)
        {
            try
            {
                var result = await _repository.PutAsync(form);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
