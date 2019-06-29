using RabbitMQ.Client;
using System;

namespace SharedLibrary
{
    public class RabbitMqRepository
    {
        private ConnectionFactory _factory;

        public RabbitMqRepository(string userName,
            string password,
            string virtualHost,
            string host)
        {
            _factory = new ConnectionFactory()
            {
                UserName = userName,
                Password = password,
                VirtualHost = virtualHost,
                HostName = host
            };
        }

        public QueueCountDTO GetMessageCount(
            string exchangeName,
            string queueName)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                try
                {
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);
                    var response = channel.QueueDeclare(queueName, true, false, false, null);
                    var messageCount = response != null ? Convert.ToInt32(response.MessageCount) : -1;

                    return new QueueCountDTO()
                    {
                        CreatedAt = DateTime.Now,
                        QueueName = queueName,
                        MessageCount = messageCount
                    };
                }

                catch (Exception ex)
                {
                    //Do nothing for now
                    throw ex;
                }
            }
        }
    }
}
