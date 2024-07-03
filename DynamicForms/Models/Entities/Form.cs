using System.ComponentModel.DataAnnotations;

namespace DynamicForms.Models.Entities;

public class Form
{
    public int Id { get; set; }
    public int? OptionId { get; set; }
    [StringLength(300)]
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<FormInput> Inputs { get; set; } = [];
    public ICollection<FormAnswer> FormAnswers { get; } = [];
}
