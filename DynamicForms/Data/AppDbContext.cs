using DynamicForms.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicForms.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    public virtual DbSet<Form> Forms { get; set; }
    public virtual DbSet<FormInput> FormInputs { get; set; }
    public virtual DbSet<FormAnswer> FormsAnswers { get; set; }
    public virtual DbSet<FormInputAnswers> FormInputAnswers { get; set; }
    public virtual DbSet<FormTypeData> FormTypeData { get; set; }
}
