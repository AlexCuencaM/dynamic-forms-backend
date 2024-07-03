using System.ComponentModel.DataAnnotations;

namespace DynamicForms.Models.Entities;

public class FormAnswer
{
    public int Id { get; set; }
    public int FormId { get; set; }
    [StringLength(300)]
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<FormInputAnswers> InputAnswers { get; set; } = [];
    public Form Form { get; set; } = null!;
}
