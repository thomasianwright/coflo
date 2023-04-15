using NodaTime;

namespace Coflo.Core.Snowflake.Models;

public class SnowflakeId
{
    public long Id { get; private set; }
    public Instant Timestamp { get; private set; }
    public long MachineId { get; private set; }
    public long Sequence { get; private set; }
    
    internal static SnowflakeId Create(long id, Instant timestamp, long machineId, long sequence)
    {
        return new SnowflakeId
        {
            Id = id,
            Timestamp = timestamp,
            MachineId = machineId,
            Sequence = sequence
        };
    }
}