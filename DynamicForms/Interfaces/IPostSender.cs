using DynamicForms.Models.DTOs;

namespace DynamicForms.Interfaces;

public interface IPostSender<DTOModel>
{
    public Task<MessageInfoDTO> PostAsync(DTOModel dto);
}