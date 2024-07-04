using DynamicForms.Interfaces;
using DynamicForms.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamicForms.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormTypeDataController(IFormTypeDataRepository repository) : ControllerBase
{
    private readonly IFormTypeDataRepository _repository = repository;
    // GET: api/<FormTypeDataController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _repository.GetAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}
