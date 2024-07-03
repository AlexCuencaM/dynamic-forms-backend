using DynamicOptions.Models.DTOs;

namespace DynamicOptions.Interfaces;

public interface IOptionsRepository
{
    Task<List<OptionDTO>> GetAsync();
}
