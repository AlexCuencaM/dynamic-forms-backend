using DynamicOptions.Data;
using DynamicOptions.Extensions;
using DynamicOptions.Interfaces;
using DynamicOptions.Models.DTOs;
using DynamicOptions.Repository;
using DynamicOptions.Senders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RabbitMQCredentials>(builder.Configuration.GetSection("RabbitMQCredentials"));
builder.Services.AddScoped<IOptionsSender, OptionsSender>();
builder.Services.AddScoped<IOptionsRepository, OptionsRepository>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
DataSeeker.Initialize(app.Services);
if (app.Environment.IsDevelopment())
{
    ApplyMigration();
}
app.Run();
void ApplyMigration()
{
    using var scope = app?.Services.CreateScope();
    var _db = scope?.ServiceProvider.GetRequiredService<AppDbContext>();
    if (_db is not null && _db?.Database.GetPendingMigrations().Count() > 0)
    {
        _db.Database.Migrate();
    }
}