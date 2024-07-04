using DynamicForms.Data;
using DynamicForms.Interfaces;
using DynamicForms.Messaging;
using DynamicForms.Models.DTOs;
using DynamicForms.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.Configure<RabbitMQCredentials>(builder.Configuration.GetSection("RabbitMQCredentials"));
var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<IInputFormRepository, InputFormRepository>();
builder.Services.AddScoped<IFormTypeDataRepository, FormTypeDataRepository>();
builder.Services.AddSingleton(new OptionsRepository(optionBuilder.Options));
builder.Services.AddHostedService<OptionsIdConsumer>();
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