using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using DynamicForms.Models.DTOs;
using DynamicForms.Interfaces;
using System.Text;
using Newtonsoft.Json;

namespace DynamicForms.Messaging.Generics;

public abstract class OptionsConsumer<DTOModel> : BackgroundService
{
    protected readonly IConnection _connection;
    protected IModel _channel;
    protected readonly string QueueName;
    protected readonly RabbitMQCredentials Credentials;
    protected IPostSender<DTOModel> _repository;
    private readonly string TopicAndQueueNames;
    protected OptionsConsumer(IConfiguration configuration, IPostSender<DTOModel> repository, IOptions<RabbitMQCredentials> credentials, string topicAndQueueNames)
    {
        _repository = repository;
        TopicAndQueueNames = topicAndQueueNames;
        QueueName = configuration.GetValue<string>($"{nameof(TopicAndQueueNames)}:{TopicAndQueueNames}") ?? throw new NullReferenceException($"Value not found: {nameof(TopicAndQueueNames)}:{TopicAndQueueNames}");
        Credentials = credentials.Value;
        _connection = new ConnectionFactory()
        {
            HostName = Credentials.HostName,
            UserName = Credentials.UserName,
            Password = Credentials.Password
        }.CreateConnection();
        _channel = DeclareQueue();
        _repository = repository;

    }
    protected virtual IModel DeclareQueue()
    {
        IModel newModel = _connection.CreateModel();
        newModel.QueueDeclare(QueueName, false, false, false, null);
        return newModel;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        _ = Consumer();
        return Task.CompletedTask;
    }
    protected virtual string Consumer()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            DTOModel newRecord = JsonConvert.DeserializeObject<DTOModel>(content) ??
                throw new InvalidDataException("Bad format on serialization");
            var result = GetResult(newRecord);
            if (!result.Success)
            {
                _channel.BasicReject(ea.DeliveryTag, false);
            }
            else
            {
                _channel.BasicAck(ea.DeliveryTag, false);
            }
        };
        return _channel.BasicConsume(QueueName, false, consumer);
    }

    protected virtual MessageInfoDTO GetResult(DTOModel newRecord)
    {
        return _repository.PostAsync(newRecord).GetAwaiter().GetResult();
    }
}