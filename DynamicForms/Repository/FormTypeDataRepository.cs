using DynamicForms.Data;
using DynamicForms.Interfaces;
using DynamicForms.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DynamicForms.Repository;

public class FormTypeDataRepository(AppDbContext context) : IFormTypeDataRepository
{
    private readonly AppDbContext _context = context;
    public async Task<List<FormTypeDataDTO>> GetAsync()
    {
        return await _context.FormTypeData
            .Select(f => new FormTypeDataDTO
            {
                Id = f.Id,
                Name = f.Name,
            })
            .ToListAsync();
    }
}
