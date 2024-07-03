﻿namespace DynamicOptions.Models.Entities;

public class Option
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
