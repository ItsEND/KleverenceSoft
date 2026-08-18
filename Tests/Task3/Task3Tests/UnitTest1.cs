using Task3;

namespace Task3Tests;

public class UnitTest1
{
    [Fact]
    public void TryParse_Format1_ShouldParseCorrectly()
    {
        const string input = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'";

        var result = LogParser.TryParse(input, out var log);

        Assert.True(result);
        Assert.NotNull(log);

        Assert.Equal("10-03-2025", log.Date);
        Assert.Equal("15:14:49.523", log.Time);
        Assert.Equal("INFO", log.Level);
        Assert.Equal("DEFAULT", log.CallerMethod);
        Assert.Equal("Версия программы: '3.4.0.48729'", log.Message);
    }
    [Fact]
    public void TryParse_Format2_ShouldParseCorrectly()
    {
        const string input = "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";

        var result = LogParser.TryParse(input, out var log);

        Assert.True(result);
        Assert.NotNull(log);

        Assert.Equal("10-03-2025", log.Date);
        Assert.Equal("15:14:51.5882", log.Time);
        Assert.Equal("INFO", log.Level);
        Assert.Equal("MobileComputer.GetDeviceId", log.CallerMethod);
        Assert.Equal("Код устройства: '@MINDEO-M40-D-410244015546'", log.Message);
    }

    [Theory]
    [InlineData("какая-то непонятная штука")]
    [InlineData("32.03.2025 15:14:49.523 INFO Message")]
    [InlineData("10.03.2025 88:14:49.523 INFO Message")]
    [InlineData("10.03.2025 15:14:49.523 TRACE Message")]
    public void TryParse_InvalidLog_ShouldReturnFalse(string input)
    {
        var result = LogParser.TryParse(input, out var log);

        Assert.False(result);
        Assert.Null(log);
    }

    [Theory]
    [InlineData("INFO", "INFO")]
    [InlineData("INFORMATION", "INFO")]
    [InlineData("WARN", "WARN")]
    [InlineData("WARNING", "WARN")]
    [InlineData("ERROR", "ERROR")]
    [InlineData("DEBUG", "DEBUG")]
    public void TryParse_ShouldNormalizeLevel(string inputLevel, string expectedLevel)
    {
        var input = $"10.03.2025 15:14:49.523 {inputLevel} message";

        var result = LogParser.TryParse(input, out var log);

        Assert.True(result);
        Assert.NotNull(log);
        Assert.Equal(expectedLevel, log.Level);
    }

    [Fact]
    public void TryParse_Format2WithoutCallerMethod_ShouldUseDefault()
    {
        const string input = "2025-03-10 15:14:51.5882| INFO|11|| message";

        var result = LogParser.TryParse(input, out var log);

        Assert.True(result);
        Assert.NotNull(log);
        Assert.Equal("DEFAULT", log.CallerMethod);
    }

    [Fact]
    public void TryParse_Format2WithPipeInsideMessage_ShouldPreserveMessage()
    {
        const string input = "2025-03-10 15:14:51.5882| INFO|11|Test.Method| A | B";

        var result = LogParser.TryParse(input, out var log);

        Assert.True(result);
        Assert.NotNull(log);

        Assert.Equal("A | B", log.Message);
    }

    [Fact]
    public void TryParse_Format1WithNonBreakingSpace_ShouldParseCorrectly()
    {
        const string input = "10.03.2025 15:14:49.523 INFORMATION\u00A0Версия программы";

        var result = LogParser.TryParse(input, out var log);

        Assert.True(result);
        Assert.NotNull(log);
        Assert.Equal("INFO", log.Level);
        Assert.Equal("Версия программы", log.Message);
    }

    [Fact]
    public void ToOutputString_ShouldSeparateFieldsWithTabs()
    {
        var log = new StandardLogEntry
        {
            Date = "10-03-2025",
            Time = "15:14:49.523",
            Level = "INFO",
            CallerMethod = "DEFAULT",
            Message = "Test message"
        };

        var result = log.ToOutputString();

        Assert.Equal("10-03-2025\t15:14:49.523\tINFO\tDEFAULT\tTest message", result);
    }
}