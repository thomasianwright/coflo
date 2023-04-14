using Coflo.Core.Snowflake.Generators;
using FluentAssertions.Extensions;
using Microsoft.Extensions.Configuration;
using Moq;
using NodaTime;
using NodaTime.Testing;

namespace Coflo.Core.Snowflake.Tests;

public class IdGeneratorTests
{
    private readonly IConfiguration _mockConfiguration;
    private readonly IdGenerator _idGenerator;
    private FakeClock _fakeClock;

    public IdGeneratorTests()
    {
        _fakeClock = new FakeClock(Instant.FromDateTimeUtc(DateTime.Parse("2021-04-01 00:00:00").AsUtc()));

        var inMemorySettings = new Dictionary<string, string> {
            {"MachineId", "1"},
        };

        _mockConfiguration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        
        _idGenerator = new IdGenerator(_mockConfiguration, _fakeClock);
    }
    
    [Fact]
    public async Task Assert_NextId_Returns_Correct_Id()
    {
        var result = await _idGenerator.NextId();
        
        Assert.Equal(1, result);
    }
}