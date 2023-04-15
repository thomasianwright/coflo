using Coflo.Core.Snowflake.Models;

namespace Coflo.Core.Snowflake.Generators;

public interface IIdGenerator
{
    Task<long> NextId();
}