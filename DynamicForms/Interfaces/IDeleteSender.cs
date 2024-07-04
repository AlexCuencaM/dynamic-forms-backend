using DynamicForms.Models.DTOs;

namespace DynamicForms.Interfaces;

public interface IDeleteSender<DTOModel>
{
    public Task<MessageInfoDTO> DeleteAsync(DTOModel dto);
}
