namespace DynamicForms.Interfaces;

public interface IPostAndDeleteSender<DTOModel> : IPostSender<DTOModel>, IDeleteSender<DTOModel>
{
}