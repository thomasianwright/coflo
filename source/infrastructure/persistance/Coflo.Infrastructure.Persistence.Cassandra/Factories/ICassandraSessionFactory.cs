using Cassandra;

namespace Coflo.Infrastructure.Persistance.Cassandra.Factories;

internal interface ICassandraSessionFactory
{
    Task<ISession> GetSessionAsync();
    ISession GetSession();
}