namespace DynamicForms.Models.DTOs;

public class MessageInfoDTO
{
    public string? Message { get; set; }
    public dynamic? Detail { get; set; }
    public bool Success { get; set; } = true;
}
