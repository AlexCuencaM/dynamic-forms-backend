using DynamicForms.Messaging.Generics;
using DynamicForms.Models.DTOs;
using DynamicForms.Repository;
using Microsoft.Extensions.Options;

namespace DynamicForms.Messaging;

public class OptionsIdConsumer(IConfiguration configuration, OptionsRepository repository, IOptions<RabbitMQCredentials> credentials) : 
    OptionsConsumer<List<OptionsDTO>>(configuration, repository, credentials, "RegisterOptions")
{
}
