using DynamicOptions.Interfaces;
using DynamicOptions.Models.DTOs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DynamicOptions.Senders;
public class OptionsSender(IOptions<RabbitMQCredentials> options) : IOptionsSender
{
    private IConnection? _connection;
    private RabbitMQCredentials Credentials = options.Value;
    public void SendMessage(object message, string queueName)
    {
        var factory = new ConnectionFactory()
        {
            HostName = Credentials.HostName,
            UserName = Credentials.UserName,
            Password = Credentials.Password

        };
        _connection = factory.CreateConnection();
        using var channel = _connection.CreateModel();
        var json = JsonConvert.SerializeObject(message);
        channel.QueueDeclare(queueName, false, false, false, null);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: string.Empty, routingKey: queueName, null, body: body);
    }
}
