using DynamicForms.Models.DTOs;

namespace DynamicForms.Interfaces;

public interface IFormRepository
{
    Task<FormDTO> GetAsync(int optionId);
    Task<MessageInfoDTO> PostAsync(FormDTO form);
    Task<MessageInfoDTO> PutAsync(FormDTO form);
    Task<MessageInfoDTO> DeleteAsync(int formId);
}
