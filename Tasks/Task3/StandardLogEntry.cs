namespace Task3;

public sealed record StandardLogEntry
{
    public required string Date { get; init; }
    public required string Time { get; init; }
    public required string Level { get; init; }
    public required string CallerMethod { get; init; }
    public required string Message { get; init; }

    public string ToOutputString()
    {
        return string.Join('\t', Date, Time, Level, CallerMethod, Message);
    }
}

