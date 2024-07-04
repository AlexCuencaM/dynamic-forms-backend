using DynamicForms.Interfaces;
using DynamicForms.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DynamicForms.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormInputController(IInputFormRepository repository) : ControllerBase
{
    private readonly IInputFormRepository _repository = repository;
    [HttpPost]
    public async Task<IActionResult> PostAsync(FormInputDTO form)
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
    public async Task<IActionResult> PutAsync(FormInputDTO form)
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
}
