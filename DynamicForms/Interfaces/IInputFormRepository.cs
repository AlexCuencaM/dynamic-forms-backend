using DynamicForms.Models.DTOs;

namespace DynamicForms.Interfaces;

public interface IInputFormRepository: IPostSender<FormInputDTO>
{
    Task<MessageInfoDTO> DeleteAsync(int id);
    public Task<MessageInfoDTO> PutAsync(FormInputDTO dto);
}
