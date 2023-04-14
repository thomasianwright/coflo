namespace Coflo.Core.Snowflake.Generators;

public interface IIdGenerator
{
    Task<long> NextId();
}