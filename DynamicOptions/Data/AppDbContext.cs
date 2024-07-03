
using DynamicOptions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicOptions.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
{
    public virtual DbSet<Option> Options { get; set; }
}
