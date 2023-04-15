using System.Diagnostics;
using System.Globalization;
using Coflo.Core.Snowflake.Generators;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.Extensions.Configuration;
using Moq;
using NodaTime;
using NodaTime.Testing;
using Xunit.Abstractions;

namespace Coflo.Core.Snowflake.Tests;

public class IdGeneratorTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IdGenerator _idGenerator;
    private readonly FakeClock _fakeClock;
    private readonly DateTimeFormatInfo _dateTimeFormat;

    public IdGeneratorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _dateTimeFormat = new CultureInfo("en-GB").DateTimeFormat;
        _fakeClock = new FakeClock(Instant.FromDateTimeUtc(DateTime.Parse("14/04/2023 00:00:00", _dateTimeFormat).AsUtc()));

        var inMemorySettings = new Dictionary<string, string>
        {
            { "MachineId", "1" },
        };

        var mockConfiguration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _idGenerator = new IdGenerator(_fakeClock, mockConfiguration);
    }

    [Fact]
    public async Task Assert_NextId_Returns_Correct_Id()
    {
        var result = await _idGenerator.NextId();

        var decodedId = IdGenerator.DecodeId(result);

        decodedId.MachineId.Should().Be(1);
        decodedId.Sequence.Should().Be(0);
        decodedId.Timestamp.Should().Be(_fakeClock.GetCurrentInstant());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task Assert_NextId_Returns_Correct_Id_When_Sequence_Overflows(int sequence)
    {
        var expectedInstant =
            Instant.FromDateTimeUtc(DateTime.Parse("14/04/2023 00:00:00", _dateTimeFormat).AsUtc() + TimeSpan.FromSeconds(sequence));
        _fakeClock.Reset(expectedInstant);
        var result = await _idGenerator.NextId();

        var decodedId = IdGenerator.DecodeId(result);
        _testOutputHelper.WriteLine(decodedId.Id.ToString());
        decodedId.MachineId.Should().Be(1);
        decodedId.Sequence.Should().Be(0);
        decodedId.Timestamp.Should().Be(expectedInstant);
    }

    [Fact]
    public async Task Assert_NextId_Returns_Correct_Id_When_Sequence_Overflows_Then_Resets()
    {
        var expectedInstant =
            Instant.FromDateTimeUtc(DateTime.Parse("14/04/2023 00:00:00", _dateTimeFormat).AsUtc() + TimeSpan.FromSeconds(5));
        _fakeClock.Reset(expectedInstant);
        var result = await _idGenerator.NextId();

        var decodedId = IdGenerator.DecodeId(result);
        _testOutputHelper.WriteLine(decodedId.Id.ToString());
        decodedId.MachineId.Should().Be(1);
        decodedId.Sequence.Should().Be(0);
        decodedId.Timestamp.Should().Be(expectedInstant);
    }
}