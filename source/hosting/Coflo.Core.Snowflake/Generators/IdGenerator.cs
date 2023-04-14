using Microsoft.Extensions.Configuration;
using NodaTime;

namespace Coflo.Core.Snowflake.Generators;

public class IdGenerator : IIdGenerator
{
    private const int _nodeIdBits = 10;
    private const int _sequenceBits = 12;
    internal const int _epochBits = 41;
    internal const int _unusedBits = 1;
    
    private readonly int _maxMachineId = (int)(Math.Pow(2, _nodeIdBits) - 1);
    private readonly int _maxSequence = (int)(Math.Pow(2, _sequenceBits) - 1);
    
    private const long _epoch = 1675209600L;
    
    private readonly int _machineId;
    private readonly IClock _clock;
    
    private long _lastTimeStamp = -1L;
    private long _sequence = 0L;
    
    private readonly Mutex _mutex;
    
    public IdGenerator(IConfiguration configuration, IClock clock)
    {
        var machineId = configuration.GetValue<int>("MachineId");
        
        if(machineId < 0 || machineId > _maxMachineId) 
        {
            throw new ArgumentException($"Node ID must be between 0 and {_maxMachineId}");
        }

        _mutex = new Mutex();
        _machineId = machineId;
        _clock = clock;
    }

    internal long GetTimestamp()
    {
        var now = _clock.GetCurrentInstant();
        var epoch = Instant.FromUnixTimeSeconds(_epoch);
        var duration = now.ToUnixTimeMilliseconds()- epoch.ToUnixTimeMilliseconds();
        
        return duration;
    }

    public async Task<long> NextId()
    {
        _mutex.WaitOne();
        
        var currentTimeStamp = GetTimestamp();

        if (currentTimeStamp < _lastTimeStamp && _sequence >= 1) throw new SystemException("System Clock Invalid");

        if (currentTimeStamp == _lastTimeStamp)
        {
            _sequence = _sequence++ & _maxSequence;

            if (_sequence == 0)
            {
                await Task.Delay(1);

                currentTimeStamp = GetTimestamp();
            }
        }
        else
        {
            _sequence = 0;
        }

        _lastTimeStamp = currentTimeStamp;

        var id =
            (currentTimeStamp << _epochBits) |
            (uint)(_machineId << _nodeIdBits) | (_sequence << _sequenceBits)
            | 1 << _unusedBits;
        
        _mutex.ReleaseMutex();
        
        return id;
    }
}