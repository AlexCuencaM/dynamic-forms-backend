using DynamicOptions.Data;
using DynamicOptions.Interfaces;
using DynamicOptions.Models.DTOs;
using DynamicOptions.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicOptions.Extensions;

public class DataSeeker
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetService<AppDbContext>() 
            ?? throw new Exception("Could not establish connection to DB");
        IOptionsSender sender = scope.ServiceProvider.GetService(typeof(IOptionsSender)) as IOptionsSender ?? throw new Exception("DI error");
        IConfiguration _configuration = scope.ServiceProvider.GetService<IConfiguration>() ?? throw new Exception("Bad DI for Iconfiguration");
        string queueName = _configuration.GetValue<string>("TopicAndQueueNames:RegisterOptions") ?? throw new NullReferenceException("Value not found: TopicAndQueueNames:RegisterOptions");
        List<Option> options = [
            new(){
                Name = "Personas",
                IsActive = true,
            },
            new(){
                Name = "Mascotas",
                IsActive = true,
            }
        ];
        if (!context.Options.Any())
        {
            context.AddRange( options );
            context.SaveChanges();
            sender.SendMessage(options.Select(option => new OptionDTO
            {
                OptionId = option.Id,
                Name = option.Name,
            }), queueName);
        }
    }
}
