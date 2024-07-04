using DynamicForms.Data;
using DynamicForms.Interfaces;
using DynamicForms.Models.DTOs;
using DynamicForms.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicForms.Repository;

public class FormRepository(AppDbContext context) : IFormRepository
{
    private readonly AppDbContext _context = context;

    public async Task<MessageInfoDTO> DeleteAsync(int formId)
    {
        var query = await _context.Forms.FirstAsync(f => f.Id == formId);
        query.IsActive = false;
        query.Inputs.ToList().ForEach(i =>
        {
            i.IsActive = false;
        });
        await _context.SaveChangesAsync();
        return new MessageInfoDTO()
        {
            Success = true,
            Message = "Form eliminado"
        };
    }

    public async Task<FormDTO> GetAsync(int optionId)
    {
        var query = _context.Forms
            .Where(f => f.Id == optionId && f.IsActive);
        return await query
            .Select(f => new FormDTO
            {
                Id = f.Id,
                OptionId = optionId,
                Name = f.Name,
                FormInputs = f.Inputs.Where(fi => fi.IsActive).Select(fi => new FormInputDTO
                {
                    FormInputId = fi.Id,
                    IsActive = fi.IsActive,
                    Label = fi.Label,
                    FormType = fi.FormTypeData.Name,
                }).ToList(),
            })
            .FirstAsync();
    }
    public async Task<MessageInfoDTO> PostAsync(FormDTO form)
    {
        var query = new Form();
        List<FormInput> options = await _context.FormInputs
            .Where(fi => form.FormInputs.Select(f => f.FormInputId).Contains(fi.Id))
            .ToListAsync();

        query.Name = form.Name;
        foreach (var item in options)
        {
            query.Inputs.Add(item);
        }
        await _context.Forms.AddAsync(query);
        await _context.SaveChangesAsync();
        return new MessageInfoDTO()
        {
            Message = "Creado"
        };
    }
    public async Task<MessageInfoDTO> PutAsync(FormDTO form)
    {
        var query = await _context.Forms.FirstAsync(f => f.Id == form.Id);
        query.Name = form.Name;
        query.Inputs.ToList().ForEach(item =>
        {
            var dtoInput = form.FormInputs.First(fi => fi.FormInputId == item.Id);
            item.IsActive = dtoInput.IsActive;
        });
        var filteredDetails = form.FormInputs.Where(x => !query.Inputs.Select(i => i.Id).Contains(x.FormInputId)).ToList();
        if (filteredDetails.Count > 0)
        {
            await _context.FormInputs.AddRangeAsync(filteredDetails.Select(x => new FormInput
            {
                FormId = query.Id,
                Label = x.Label,
                FormTypeDataId = x.FormTypeDataId ?? throw new Exception("Bad datatype"),
                IsActive = x.IsActive,
            }));
        }
        await _context.SaveChangesAsync();
        return new MessageInfoDTO()
        {
            Message = "Modificado"
        };
    }
}
