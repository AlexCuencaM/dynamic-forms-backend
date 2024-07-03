namespace DynamicOptions.Interfaces;

public interface IOptionsSender
{
    void SendMessage(object message, string queueName);
}
