using System.Data;
using Coflo.Core.Snowflake.Models;
using Microsoft.Extensions.Configuration;
using NodaTime;

namespace Coflo.Core.Snowflake.Generators;

public class IdGenerator : IIdGenerator
{
    private readonly IClock _clock;
    private readonly long _machineId;

    private const long TimestampBits = 41;
    private const long SequenceBits = 8;
    private const long MachineIdBits = 63 - TimestampBits - SequenceBits;
    private const long Epoch = 1_638_400_000_000L;
    private long _lastTimestamp = -1L;
    private long _sequence = 0L;

    private readonly Mutex _mutex;
    
    public IdGenerator(IClock clock, IConfiguration configuration)
    {
        _clock = clock;
        _machineId = configuration.GetValue<long>("MachineId");
        
        _mutex = new Mutex();
    }
    
    public Task<long> NextId()
    {
        var timestamp = GetTimestamp();
        
        _mutex.WaitOne();
        
        if (timestamp == _lastTimestamp)
        {
            _sequence = (_sequence + 1) & ((1 << (int) SequenceBits) - 1);
            
            if (_sequence == 0)
            {
                timestamp = GetNextTimestamp();
            }
        }
        else
        {
            _sequence = 0;
        }
        
        _lastTimestamp = timestamp;
        
        _mutex.ReleaseMutex();
        
        return Task.FromResult(EncodeId(_clock.GetCurrentInstant(), _machineId, _sequence));
    }
    
    private long GetTimestamp()
    {
        return _clock.GetCurrentInstant().ToUnixTimeMilliseconds() - Epoch;
    }
    
    private long GetNextTimestamp()
    {
        var timestamp = GetTimestamp();
        
        while (timestamp <= _lastTimestamp)
        {
            timestamp = GetTimestamp();
        }

        return timestamp;
    }
    
    internal static long EncodeId(Instant timestamp, long machineId, long sequence)
    {
        var timestampDelta = timestamp.ToUnixTimeMilliseconds() - Epoch;
        var id = (timestampDelta << (int) (SequenceBits + MachineIdBits)) | (machineId << (int) SequenceBits) | sequence;
        
        return id;
    }
    
    public static SnowflakeId DecodeId(long id)
    {
        var sequence = id & ((1 << (int) SequenceBits) - 1);
        var machineId = (id >> (int) SequenceBits) & ((1 << (int) MachineIdBits) - 1);
        var timestamp = (id >> (int) (SequenceBits + MachineIdBits)) + Epoch;

        return SnowflakeId.Create(id, Instant.FromUnixTimeMilliseconds(timestamp), machineId, sequence);
    }
}