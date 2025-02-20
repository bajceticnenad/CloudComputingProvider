using StackExchange.Redis;

namespace CloudComputingProvider.Services.Interfaces
{
    public interface IRedisConnectorHelper
    {
        ConnectionMultiplexer GetConnection();
    }
}
