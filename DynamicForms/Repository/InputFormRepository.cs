using DynamicForms.Data;
using DynamicForms.Interfaces;
using DynamicForms.Models.DTOs;
using DynamicForms.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace DynamicForms.Repository;

public class InputFormRepository(AppDbContext dbContext) : IInputFormRepository
{
    private readonly AppDbContext _context = dbContext;

    public async Task<MessageInfoDTO> DeleteAsync(int id)
    {
        FormInput formInput = await _context.FormInputs.FirstAsync(f => id == f.Id);
        _context.FormInputs.Remove(formInput);
        await _context.SaveChangesAsync();
        return new MessageInfoDTO
        {
            Message = "borrado",
            Detail = id
        };
    }

    public async Task<MessageInfoDTO> PutAsync(FormInputDTO dto)
    {
        FormInput formInput = await _context.FormInputs.FirstAsync(f => dto.FormInputId == f.Id);
        formInput.Label = dto.Label;
        formInput.FormTypeDataId = dto.FormTypeDataId ?? throw new Exception("Bad datatype");
        formInput.IsActive = dto.IsActive;
        await _context.SaveChangesAsync();
        return new()
        {
            Message = "Modificado",
            Detail = dto.FormInputId,
        };
    }
    public async Task<MessageInfoDTO> PostAsync(FormInputDTO dto)
    {
        var result = await _context.FormTypeData.FirstAsync(t => t.Id == dto.FormTypeDataId);
        var newRecord = new FormInput
        {
            FormId = dto.FormId,
            Label = dto.Label,
            FormTypeDataId = dto.FormTypeDataId ?? throw new Exception("Bad datatype"),
            IsActive = dto.IsActive,
        };
        await _context.FormInputs.AddAsync(newRecord);
        await _context.SaveChangesAsync();
        return new()
        {
            Message = "Creado",
            Detail = new
            {
                newRecord.Id,
                formType = result.Name,
            }
        };
    }

}
