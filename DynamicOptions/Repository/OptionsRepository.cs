using DynamicOptions.Data;
using DynamicOptions.Interfaces;
using DynamicOptions.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DynamicOptions.Repository;

public class OptionsRepository(AppDbContext context): IOptionsRepository
{
    private readonly AppDbContext _context = context;
    public async Task<List<OptionDTO>> GetAsync()
    {
        return await _context.Options
            .Where(o => o.IsActive)
            .Select(o => new OptionDTO
            {
                Name = o.Name,
                OptionId = o.Id,
            })
            .ToListAsync();
    }
}
