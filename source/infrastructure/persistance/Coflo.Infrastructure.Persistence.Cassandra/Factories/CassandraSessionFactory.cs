using Cassandra;

namespace Coflo.Infrastructure.Persistance.Cassandra.Factories;

internal class CassandraSessionFactory : ICassandraSessionFactory
{
    private readonly ICluster _cluster;

    public CassandraSessionFactory(ICluster cluster)
    {
        _cluster = cluster;
    }
    
    public Task<ISession> GetSessionAsync()
    {
        return _cluster.ConnectAsync();
    }

    public ISession GetSession()
    {
        return _cluster.Connect();
    }
}