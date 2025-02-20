using CloudComputingProvider.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace CloudComputingProvider.Services.Helpers
{
    public class RedisConnectorHelper : IRedisConnectorHelper
    {
        #region PrivateFields
        private readonly IConfiguration _configuration;
        private Lazy<ConnectionMultiplexer> lazyConnection;
        private readonly string _redisCacheConnectionUrl;
        #endregion PrivateFields

        #region Public Constructor
        //public RedisConnectorHelper(IOptions<RedisCache> options)
        //{
        //    _redisCache = options.Value;
        //    lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        //    {
        //        return ConnectionMultiplexer.Connect(_redisCache.RedisCacheConnectionUrl);
        //    });
        //}
        public RedisConnectorHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _redisCacheConnectionUrl = _configuration?.GetSection("Redis")["RedisCacheConnectionUrl"];
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(_redisCacheConnectionUrl);
            });
        }
        #endregion Public Constructor

        #region Methods

        public ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public ConnectionMultiplexer GetConnection()
        {
            return lazyConnection.Value;
        }
        #endregion Methods
    }
}
