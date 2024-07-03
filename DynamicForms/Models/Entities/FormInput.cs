using System.ComponentModel.DataAnnotations;

namespace DynamicForms.Models.Entities;

public class FormInput
{
    public int Id { get; set; }
    [StringLength(300)]
    public string Label { get; set; } = string.Empty;
    //public object? Value { get; set; }
    public int FormTypeDataId { get; set; }
    public bool IsActive { get; set; }
    public FormTypeData FormTypeData { get; set; } = null!;
    public int FormId { get; set; }
    public Form Form { get; set; } = null!;
}
