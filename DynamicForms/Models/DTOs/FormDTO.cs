using System.ComponentModel.DataAnnotations;

namespace DynamicForms.Models.DTOs;

public class FormDTO
{
    public int? Id { get; set; }
    public int? OptionId { get; set; }
    [StringLength(300)]
    public string Name { get; set; } = string.Empty;
    public List<FormInputDTO> FormInputs { get; set; } = [];
}

public class FormInputDTO
{
    public int FormId { get; set; }
    public bool IsActive { get; set; }
    public int FormInputId { get; set; }
    public string Label { get; set; } = string.Empty;
    public int? FormTypeDataId { get; set; }
    public string FormType { get; set; } = string.Empty;
}
