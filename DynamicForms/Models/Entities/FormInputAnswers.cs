using System.ComponentModel.DataAnnotations;

namespace DynamicForms.Models.Entities;

public class FormInputAnswers
{
    public int Id { get; set; }
    [StringLength(300)]
    public string Label { get; set; } = string.Empty;
    [StringLength(300)]
    public string? Value { get; set; }
    public int FormTypeDataId { get; set; }
    public FormTypeData FormTypeData { get; set; } = null!;
}
