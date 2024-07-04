using DynamicForms.Models.DTOs;

namespace DynamicForms.Interfaces;

public interface IFormTypeDataRepository
{
    Task<List<FormTypeDataDTO>> GetAsync();
}
